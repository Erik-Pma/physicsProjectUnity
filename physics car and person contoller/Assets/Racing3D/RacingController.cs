using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingController : MonoBehaviour
{
    public enum CarState {Drive, Reverse, Brake, Park }


    Rigidbody rb;
    private float averageRPM = 0f;
    private bool isMovingreverse = false;
    private CarState state;

    [SerializeField] float VerticalInput = 0f;
    [SerializeField] float HorizontalInput = 0f;
    bool usingBrakes = false;

    [SerializeField]WheelAxisExample[] axles;

    [Header("engine stats")]
    public float maxMotorTorque;
    public float maxSpeed;
    public AnimationCurve motorCurve;// use to adjeust torque amount

    [Header("breaking stats")]
    public float normalDamping;
    public float brakeDamping;//strech of brakes

    public float brakeThreshold;
    public float parkingThreshold;

    [Header("steering stats")]
    public float maxSteeringAngle;
    public float minSteeringAngle;
    public AnimationCurve animationCurve;
    public float steeringWheelSpeed;

    public float antiRollStrength;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        OnMove();
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            OnBrake();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            OnBrake();
        }
        UpdateCarState();
        UpdateControlls();
    }
    private void OnMove()
    {
        VerticalInput = Input.GetAxis("Vertical");
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    void OnBrake() 
    {
        usingBrakes = !usingBrakes;
    }

    private void UpdateCarState() 
    {
        if (usingBrakes)
        {
            state = CarState.Brake;
        }
        else if (averageRPM > parkingThreshold)
        {
            if (VerticalInput <= -brakeThreshold)
            {
                state = CarState.Brake;
            }
            else
            {
                state = CarState.Drive;
            }
        }
        else if (averageRPM < -parkingThreshold)
        {
            if (VerticalInput >= -brakeThreshold)
            {
                state = CarState.Brake;
            }
            else
            {
                state = CarState.Reverse;
            }
        }
        else 
        {
            state = CarState.Park;
        }
    }

    void UpdateControlls() 
    {
        float speed = rb.velocity.magnitude;
        float speedPercent = Mathf.Abs(speed/maxSpeed);

        float totalRPM =0;
        int numberOfWheels = 4;
        foreach (WheelAxisExample axle in axles) 
        {
            if (axle.isSteering) 
            {
                float angle = minSteeringAngle + (animationCurve.Evaluate(speedPercent) * (maxSteeringAngle - minSteeringAngle));
                float steering = angle * HorizontalInput * Time.fixedDeltaTime * steeringWheelSpeed;
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }

            //braking
            axle.leftWheel.wheelDampingRate = normalDamping;
            axle.rightWheel.wheelDampingRate = normalDamping;
            if (state == CarState.Brake)
            {
                axle.leftWheel.wheelDampingRate = brakeDamping;
                axle.rightWheel.wheelDampingRate = brakeDamping;
            }
            // acceleration
            else if (axle.isMotor)
            {
                if (speed >= maxSpeed)
                {
                    axle.leftWheel.motorTorque = 0;
                    axle.rightWheel.motorTorque = 0;
                }
                else 
                {
                    float horsepow = VerticalInput * Time.fixedDeltaTime * maxMotorTorque *motorCurve.Evaluate(speedPercent);
                    axle.leftWheel.motorTorque = horsepow;
                    axle.rightWheel.motorTorque = horsepow;
                }
            }

            //TODO update anti rolling and visules for axis
            axle.updateAniRollBar(antiRollStrength,rb);
            axle.UpdateVisuals();

            totalRPM +=axle.leftWheel.rpm + axle.rightWheel.rpm;
            numberOfWheels += 2;
            
        }
        averageRPM = totalRPM / numberOfWheels;
        isMovingreverse = averageRPM < -parkingThreshold;
    }
}
