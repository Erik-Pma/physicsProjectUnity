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

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            RayHit();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            shoot2();
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
    public void shoot2()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, 10f, out hit))
        {
            Transform objectHit = hit.transform;

            print(objectHit.name);
            controller.health -= controller.health;
            hit.transform.TryGetComponent<Rigidbody>(out Rigidbody rb);
            rb.AddExplosionForce(forcePower, hit.point, 10f,1f,ForceMode.Impulse);
        }
        
    }
    public void shoot3() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) 
        {
            lastpo = hit.point;
            Collider[] colliders = Physics.OverlapSphere(hit.point, 10f, mask.value);
            foreach (Collider collider in colliders)
            {
                Debug.Log(collider.gameObject.name);

                RagdollController rc;
                if (collider.TryGetComponent<RagdollController>(out rc)) 
                {
                    rc.BeRagdoll();
                }

                Rigidbody[] limbs = rc.GetComponentsInChildren<Rigidbody>();
                for (int i = 0; i < limbs.Length; i++)
                {
                    limbs[i].AddExplosionForce(800f, hit.point, 100f);
                }
            };
        }
    }
    public void shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            lastpo = hit.point;
            Collider[] colliders = Physics.OverlapBox(hit.point, Vector3.one,Quaternion.identity);
            foreach (Collider collider in colliders)
            {
                Debug.Log(collider.gameObject.name);

                RagdollController rc;
                if (collider.TryGetComponent<RagdollController>(out rc))
                {
                    rc.BeRagdoll();
                }

                Rigidbody[] limbs = rc.GetComponentsInChildren<Rigidbody>();
                for (int i = 0; i < limbs.Length; i++)
                {
                    limbs[i].AddExplosionForce(800f, hit.point, 100f);
                }
            };
        }
    }
    public void Teleport() 
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Vector3 ClosestPoint = Physics.ClosestPoint(hit.point,objectHit.GetComponent<Collider>(),transform.position, Quaternion.identity);


        }
    }

    
}
