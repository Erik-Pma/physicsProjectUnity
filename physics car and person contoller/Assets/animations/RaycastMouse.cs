using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMouse : MonoBehaviour
{
    
    public RagdollController controller;
    public float forcePower = 100;
    RaycastHit hit;
    Vector3 lastpo;
    
    public LayerMask mask;
    float height;

    public GameObject grenades;

    public Transform spot;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            RayHit();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Grenade();
    }
    /// <summary>
    /// makes ray at the camera and tell you what it hits
    /// 
    /// </summary>
    public void RayHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            print(objectHit.name);

            if (objectHit.name.Equals("mixamorig:Head") || objectHit.name.Equals("mixamorig:Neck")) 
            {
                controller.health -= controller.health;
            }
            if (objectHit.name.Equals("mixamorig:Spine1")|| objectHit.name.Equals("mixamorig:Spine2")|| objectHit.name.Equals("mixamorig:Hips"))
            {
                controller.health -= 3;
            }
        }

    }
    public void Grenade() 
    {
        Instantiate(grenades,spot.position,spot.rotation);
        
    }

    
}
