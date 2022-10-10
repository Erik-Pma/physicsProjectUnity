using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRender : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] Transform[] controlPoints;

    // Start is called before the first frame update
    void Start()
    {
        line= GetComponent<LineRenderer>();
        line.positionCount = controlPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            line.SetPosition(i, controlPoints[i].position);
        }
    }
}
