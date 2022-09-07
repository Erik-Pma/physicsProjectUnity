using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformCharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    float HorizontalInput;
    public float speed = 5;
    bool onGround;
    float groundCheckDistance = 1.2f;
    public LayerMask groundLayer;
    RaycastHit2D hit;
    bool OnGround 
    {
        get { return onGround; }
        set { if (value != onGround) 
            {
                if (value == true)
                {
                    transform.SetParent(hit.transform, true);
                }
                else 
                {
                    transform.SetParent(null, true);
                    //happens when i jump
                }
            } onGround = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Oldway for input
    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
    }
    /*
    private void OnMove()
    {
        HorizontalInput = value.Get<float>();   
    }
    */
    private void FixedUpdate()
    {
        GroundCheck();
        rb.velocity  = new Vector2 ( HorizontalInput * speed,rb.velocity.y);
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            print("jump");
            
            if (onGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
        }
    }
    void GroundCheck() 
    {
       hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {
            OnGround = true;
        }
        else 
        {
            OnGround = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckDistance));
    }

}
