using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// moves the platform on the waypoints
/// </summary>
public class MovePlatform : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponentInChildren<TitanfallMovement>()) 
        {
            CharacterController cc = GetComponentInChildren<CharacterController>();
            cc.Move(rb.velocity * Time.deltaTime);

        }
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f) 
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) 
            {
                currentWaypointIndex = 0;
            }
        }


        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        
    }
}
