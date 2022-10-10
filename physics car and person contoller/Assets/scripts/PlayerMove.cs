using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float speed = 2f;
    float maxSpeed = 5f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            rb.AddForce((transform.forward * speed/10), ForceMode.Impulse);
            if (rb.velocity.magnitude > maxSpeed)
            {
               
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce((transform.forward * -speed/10), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * speed * 10 * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * speed * 10 *  Time.deltaTime);
    }
}
