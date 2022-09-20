using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMeshAgent : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //gets the ai nave mesh agent
        agent = GetComponent<NavMeshAgent>();

    }

    private void OnDrawGizmos()
    {
        if (velocity) 
        {
            //draws a green line of the current velocity
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }
        if (desiredVelocity)
        {
            // draws aline of the deside veelocity
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }
        // draws the noade path that it will take
        if (path) 
        {
            Gizmos.color = Color.black;
            //get the agent path
            NavMeshPath agentPath = agent.path;
            //get the current previous corner
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners) 
            {
                //draw a line between the to 
                Gizmos.DrawLine(prevCorner, corner);
                // creates a sphere and draw a sphere
                Gizmos.DrawSphere(corner, 0.1f);
                prevCorner = corner;

            }
        }
    }
}
