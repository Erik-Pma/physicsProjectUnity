using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public RagdollController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
            if (objectHit.name.Equals("mixamorig:Spine1") || objectHit.name.Equals("mixamorig:Spine2") || objectHit.name.Equals("mixamorig:Hips"))
            {
                controller.health -= 3;
            }
        }

    }
}
