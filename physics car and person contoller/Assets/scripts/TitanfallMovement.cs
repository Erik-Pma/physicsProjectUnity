using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TitanfallMovement : MonoBehaviour
{
    CharacterController controller;
    [Header("ground check")]
    public Transform groundCheck;

    public LayerMask groundMask;

    public LayerMask wallMask;

    Vector3 move;
    Vector3 input;
    Vector3 Yvelocity;
    Vector3 forwardDirection;

    
    [Header("Debug Menu")]
    [ReadOnly]
    public int jumpCharges;
    [ReadOnly]
    [SerializeField]bool isGrounded;
    [ReadOnly]
    [SerializeField] bool isSprinting;
    [ReadOnly]
    [SerializeField] bool isCrouching;
    [ReadOnly]
    [SerializeField] bool isSliding;
    [ReadOnly]
    [SerializeField] bool isWallRunning;
    [ReadOnly]
    [SerializeField]float speed;

    [Header("speed values")]
    public float runSpeed;

    public float sprintSpeed;

    public float crouchSpeed;

    public float airSpeed;

    public float slideSpeedIncrease;

    public float slideSpeedDecrease;

    public float wallSpeedIncrease;

    public float wallSpeedDecrease;

    

    float gravity;

    [SerializeField] TMP_Text speedText;
    [Header("game stats")]
    public float normalGravity;

    public float wallRunGravity;

    public float jumpHeight;

    float startHeight;

    float crouchHeight = 0.5f;

    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);

    Vector3 standingCenter = new Vector3(0, 0, 0);

    float slideTimer;

    public float maxSlideTimer;

    bool hasWallRun = false;

    bool onLeftWall;

    bool onRightWall;
    
    RaycastHit leftWallHit;

    RaycastHit rightWallHit;

    Vector3 wallNormal;

    Vector3 lastWallNormal;


    public Camera playerCamera;

    float normalFov;

    public float specialFov;

    public float cameraChangeTime;

    public float wallRunTilt;

    public float tilt;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startHeight = transform.localScale.y;
        normalFov = playerCamera.fieldOfView;
    }
    /// <summary>
    /// takes in a speed paremeter an adds it to the vurretn speed float this can go above the ground speed cap
    /// </summary>
    /// <param name="speedIncrease"></param>
    void IncreaseSpeed(float speedIncrease) 
    {
        speed += speedIncrease;
    }
    /// <summary>
    /// takes a speed paremeter an subtracts it form the speed float
    /// </summary>
    /// <param name="speedDecrease"></param>
    void DecreaseSpeed(float speedDecrease) 
    {
        speed -= speedDecrease * Time.deltaTime;
    }
    /// <summary>
    /// handles player inputs
    /// </summary>
    void HandleInput() 
    {
        if(!isSliding)
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);
        
        if (Input.GetKeyUp(KeyCode.Space) && jumpCharges > 0) //jumps if you press the space key and you have a jump charage
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C)) //croches if you hit the c key
        {
            Crouch();
            
        }
        if (Input.GetKeyUp(KeyCode.C))//uncroches when you are done pressing the c key;
        {
            ExitCrouch();
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) //sprints when you hit the sprint key
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //unsprints when you let go the sprint key
        {
            isSprinting = false;
        }
    }
    /// <summary>
    /// aplly effect to the camera when you are wall runing or sliding 
    /// </summary>
    void CameraEffect() 
    {
        float fov = isWallRunning ? specialFov : isSliding ? specialFov : normalFov;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, cameraChangeTime *Time.deltaTime);//sets the player fov to be wider when sliding

        if (isWallRunning) // set cammer tilt when on the wall depending on direction
        {
            if (onRightWall) 
            {
                tilt = Mathf.Lerp(tilt,wallRunTilt,cameraChangeTime * Time.deltaTime);
            }
            if (onLeftWall) 
            {
                tilt = Mathf.Lerp(tilt, -wallRunTilt, cameraChangeTime * Time.deltaTime);
            }
        }
        if (!isWallRunning) 
        {
            tilt = Mathf.Lerp(tilt,0,cameraChangeTime*Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckWallRun();//TODO: this is proably in the wrong spot
        //makes a movement type based on state
        if (isGrounded && !isSliding)
        {
            GroundMovement();
            isWallRunning = false;//HACK: this is just a hack fix to make sure you cant wal run on the ground
        }
        else if (!isGrounded && !isWallRunning)
        {
            AirMovment();
        }
        if(isSliding)
        {
            
            SlideMovement();
            DecreaseSpeed(slideSpeedDecrease);
            slideTimer -= 1f * Time.deltaTime;
            if (slideTimer < 0)
            {

                
                isSliding = false;
            }
        }
        else if (isWallRunning && !isGrounded)
        {
            WallRunMovment();
            DecreaseSpeed(wallSpeedDecrease);
        }
        if (isSprinting && isCrouching)
        {
            isSprinting = false;
        }

        //checks to see if you are still on the ground in the game
        checkGround();
        // change camera based on the state
        CameraEffect();
        speedText.text ="speed: " + move.magnitude.ToString("n2");
    }
    private void FixedUpdate()
    {
        
        controller.Move(move * Time.deltaTime);
        //applys
        ApplyGravity();
    }
    /// <summary>
    /// the moment that is used on the ground crouch walking and runing state
    /// </summary>
    void GroundMovement() 
    {
        speed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : runSpeed;// the speed that is used for the current ground state
        if (input.x != 0)
        {
            move.x += input.x * speed;
        }
        else 
        {
            move.x = 0f;
        }
        if (input.x != 0)
        {
            move.z += input.z * speed;
        }
        else
        {
            move.z = 0f;
        }

        move = Vector3.ClampMagnitude(move, speed);// clap the speed so you cant move fater moving sideways
    }/// <summary>
    /// the movement that is applyed in the air state
    /// </summary>
    void AirMovment() 
    {
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;

        move = Vector3.ClampMagnitude(move, speed);// clap the speed so you cant move fater moving sideways
    }
    /// <summary>
    /// movment value apllyed whell in the slide state
    /// </summary>
    void SlideMovement() 
    {
        move += forwardDirection;
        move = Vector3.ClampMagnitude(move, speed);// clap the speed so you cant move fater moving sideways
    }
    /// <summary>
    /// the movement values that are used in a wall run
    /// </summary>
    void WallRunMovment() 
    {
        if (input.z > (forwardDirection.z - 10f) && input.z < (forwardDirection.z + 10f))
        {
            move += forwardDirection;

        } else if(input.z < (forwardDirection.z -10f) && input.z>(forwardDirection.z +10f))
        {
            move.x = 0f;
            move.z = 0f;
            ExitWallRun();
        }
        move.x += input.x * airSpeed;

        move = Vector3.ClampMagnitude (move, speed);// clap the speed so you cant move fater moving sideways
    }
    /// <summary>
    /// ground check based on sphere cast
    /// </summary>
    void checkGround() 
    {
        if(!isCrouching)
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        if (isGrounded) 
        {
            jumpCharges = 1;
            hasWallRun = false;
        }
    }
    /// <summary>
    /// checks to see if you are wall running
    /// </summary>
    void CheckWallRun() 
    {
        onLeftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 0.7f, wallMask);
        onRightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 0.7f, wallMask);

        if ((onRightWall || onLeftWall) && !isWallRunning) 
        {
            TestWallRun();
        }
        if ((!onRightWall && !onLeftWall) && !isWallRunning) 
        {
            ExitWallRun();
        }
    }
    /// <summary>
    /// some constraints to wall running
    /// </summary>
    void TestWallRun() 
    {
        wallNormal = onLeftWall ? leftWallHit.normal : rightWallHit.normal;
        if (hasWallRun)
        {
            float wallAngle = Vector3.Angle(wallNormal, lastWallNormal);
            if (wallAngle > 15)
            {
                WallRun();
            }
        }
        else 
        {
            WallRun();
            hasWallRun = true;
            lastWallNormal = wallNormal;
        }
    }
    /// <summary>
    /// the gravity type to use based on game state
    /// </summary>
    void ApplyGravity() 
    {
        gravity = isWallRunning ? wallRunGravity :normalGravity;
        
        Yvelocity.y += gravity * Time.deltaTime;
        controller.Move(Yvelocity * Time.deltaTime);
    }
    /// <summary>
    /// makes th player jump and add speed to the vector
    /// </summary>
    void Jump() 
    {

        if (!isGrounded && !isWallRunning)
            jumpCharges--;
        else if (isWallRunning) 
        {
            ExitWallRun();//jump out of a wall run to set the camera back to normal
            IncreaseSpeed(wallSpeedIncrease);// give you speed when leaving the wall run to make air movement feel better
        }
        Yvelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
    }
    /// <summary>
    /// what to do when you enter crouch
    /// </summary>
    void Crouch() 
    {
        controller.height = crouchHeight;
        controller.center = crouchingCenter;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);//makes your character smaller
        isCrouching = true;
        if (speed > runSpeed) //if you crouch at speed lets you slide to help maintain speed
        {
            isSliding = true;
            forwardDirection = transform.forward;
            if (isGrounded) 
            {
                IncreaseSpeed(slideSpeedIncrease);//add a speed to the inital slide  to help you move at the start
            }
            if (slideTimer < 0) 
            slideTimer = maxSlideTimer;//sets the slide length be for you become crouched
            
        }
    }
    /// <summary>
    /// what to do when you leave crouch
    /// </summary>
    void ExitCrouch()
    {
        controller.height = (startHeight * 2 );
        controller.center = standingCenter;
        transform.localScale = new Vector3(transform.localScale.x, startHeight, transform.localScale.z);
        isCrouching = false;
        isSliding = false;
    }/// <summary>
    /// starts your wall run
    /// </summary>
    void WallRun() 
    {
        isWallRunning = true;
        jumpCharges = 1;// gives you ajump charge back
        IncreaseSpeed(wallSpeedIncrease);//adds an intial speed to make plares want to wall run
        Yvelocity = new Vector3 (0, 0, 0);

        
        forwardDirection = Vector3.Cross(wallNormal, Vector3.up);

        if (Vector3.Dot(forwardDirection, transform.forward) < 0) 
        {
            forwardDirection = -forwardDirection;
        }
    }
    /// <summary>
    /// stops you from wall running
    /// </summary>
    void ExitWallRun() 
    {
        
        isWallRunning = false;
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, 0.2f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.7f);
    }
}
