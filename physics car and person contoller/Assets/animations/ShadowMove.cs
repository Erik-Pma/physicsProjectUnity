using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMove : MonoBehaviour
{
    Light[] lights;
    [SerializeField] bool[] isDirectional;
    RaycastHit hit;
    Vector3 ClosestPoint;
    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<Light>();
        isDirectional = new bool[lights.Length];

        for (int i = 0; i < lights.Length; i++) 
        {
            if (lights[i].type == LightType.Directional) 
            {
                isDirectional[i] = true;
            }
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            TeleportPlayer();
        }
    }


    bool IsInShadow(Vector3 point) 
    {
        for (int i = 0; i < lights.Length; i++) 
        {
            float dist = Vector3.Distance(point, lights[i].transform.position);
            Vector3 dir = point -lights[i].transform.position;
            Ray ray = new Ray(lights[i].transform.position, dir);

            if (!Physics.Raycast(ray, dist)) 
            {
                return false; // if we hit something dont need ot keep checking
            }
        }
        return true;
    }

    void GetPoint() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            ClosestPoint = Physics.ClosestPoint(hit.point, objectHit.GetComponent<Collider>(), transform.position, Quaternion.identity);
            
        }
        
    }
    void TeleportPlayer() 
    {
        GetPoint();
        if (IsInShadow(ClosestPoint))
        {
            transform.position = new Vector3 (ClosestPoint.x,ClosestPoint.y+2,ClosestPoint.z);
        }
    }


}
