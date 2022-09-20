using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minX = -60f;
    public float maxX = 60f;

    public float sensitivity;

    public Camera cam;

    float rotY = 0f;
    float rotX = 0f;

    TitanfallMovement move;
    // Start is called before the first frame update
    void Start()
    {
        //lock cursor and set it to flase so you can move the screen more easily
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // get the titanfall controller
        move = GetComponent<TitanfallMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the camera and make it move to the mouse
        rotY += Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;
        //clamp the rotaion to it does over rotate
        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotX, 0, move.tilt);
    }
}
