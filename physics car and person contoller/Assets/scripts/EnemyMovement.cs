using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;

    Animator animator;
    NavMeshAgent agent;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer that moves the character
        timer -= Time.deltaTime;
        if(timer <= 0.0f) 
        {
            // the distance the animator travels
            float distance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (distance > maxDistance*maxDistance)
            {
                agent.destination = playerTransform.position;
            }
            timer = maxTime;
        }
        //set the speed of the character
        animator.SetFloat("speed", agent.velocity.magnitude);
    }
}
