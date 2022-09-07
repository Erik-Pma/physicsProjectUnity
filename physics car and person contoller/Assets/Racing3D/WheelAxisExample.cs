using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelAxisExample
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool isMotor;
    public bool isSteering;

    public void updateAniRollBar(float antiRollStrength, Rigidbody carBody) 
    {
        float leftbar =1;
        float rightbar = 1;

        //messure the pressure on each side

        WheelHit hit;
        bool leftGrounded = leftWheel.GetGroundHit(out hit);
        if (leftGrounded) 
        {
            leftbar = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;
        }
        bool rightGrounded = rightWheel.GetGroundHit(out hit);
        if (rightGrounded) 
        {
            rightbar = (-rightWheel.transform.InverseTransformPoint(hit.point).y -rightWheel.radius)/rightWheel.suspensionDistance;
        }
        //use the force
        float antiRollForce = (leftbar - rightbar) * antiRollStrength;
        if (leftGrounded) 
        {
            carBody.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        }
        if (rightGrounded)
        {
            carBody.AddForceAtPosition(rightWheel.transform.up * -antiRollForce, rightWheel.transform.position);
        }
    }

    void updateWheelVisuals(WheelCollider collider) 
    {
        if (collider.transform.childCount == 0) 
        {
            return;
        }
        Transform visual = collider.transform.GetChild(0);
        Vector3 posistion;
        Quaternion rotation;
        collider.GetWorldPose(out posistion, out rotation);
        visual.transform.position = posistion;
        visual.transform.rotation = rotation * Quaternion.AngleAxis(90,Vector3.up);
    }

    public void UpdateVisuals()
    {
        updateWheelVisuals(leftWheel);
        updateWheelVisuals(rightWheel);
    }
}
