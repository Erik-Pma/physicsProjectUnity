using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Up - Down")]
    [SerializeField] [Range(1, 10f)] float distance = 3f;
    [Header("side to side")]
    //[SerializeField] [Range(1, 10f)] float distanceSide = 3f;
    float endy;
    float starty;
    float endx;
    float startx;
    [Header("speed")]
    [SerializeField] [Range(1, 10f)] float speed = 1;
    bool isMoving = true;
    bool isMovingSide = true;

    // Start is called before the first frame update
    void Start()
    {
        starty = transform.position.y;
        startx = transform.position.x;
        endy = starty + distance;
        endx = startx + distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnMove();
    }
    private void OnMove()
    {
        if (isMoving)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y > endy)
            {
                isMoving = false;
                transform.position = new Vector3(transform.position.x, endy, 0);
            }
            
        }
        else 
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if (transform.position.y < starty)
            {
                isMoving = true;
                transform.position = new Vector3(transform.position.x, starty, 0);
            }
        }

        if (isMovingSide) 
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x > endx)
            {
                isMovingSide = false;
                transform.position = new Vector3(transform.position.y, endx, 0);
            }
            else
            {
                transform.position -= Vector3.right * speed * Time.deltaTime;
                if (transform.position.x < startx)
                {
                    isMovingSide = true;
                    transform.position = new Vector3(transform.position.y, startx, 0);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (endy == 0)
        {
            Gizmos.DrawCube(transform.position + (Vector3.up * distance), transform.lossyScale);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.up * distance));
        }
        else 
        {
            Gizmos.DrawCube(new Vector3(transform.position.x,endy,0.1f),transform.lossyScale);
            Gizmos.DrawCube(new Vector3(transform.position.x, starty, 0.1f), transform.lossyScale);

        }
    }
}
