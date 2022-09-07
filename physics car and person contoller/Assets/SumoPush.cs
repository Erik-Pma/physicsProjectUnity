using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumoPush : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    float radius = 3;
    bool isActive;
    GameObject wave;
    LayerMask sumolayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive) 
        {
            ray = new Ray(transform.position, transform.forward);
            if(Physics.SphereCast(ray, radius,out hit , 10f, sumolayer.value) )
            {
                
            }
        }
    }

    public void Ha() 
    {
        isActive = !isActive;
        wave.SetActive(isActive);
    }
    public void GetMidPoint(Vector3 endpoint) 
    {
        float dist = Vector3.Distance(transform.position, endpoint);
        VisualizeBeam(transform.position + (transform.forward * (dist / 2f)), dist);
    }
    void VisualizeBeam(Vector3 midPoint,float length) 
    {
        wave.transform.position = midPoint;
        wave.transform.localScale = new Vector3(radius, length, radius);
    }
}
