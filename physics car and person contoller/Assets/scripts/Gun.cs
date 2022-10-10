using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float ImpachtForce = 30f;

    public Camera fpsCam;

    //public ParticleSystem muzzleFlash;
    public GameObject bulletDecal;

    public GameObject grenades;

    public Transform spot;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
        
    }
    /// <summary>
    /// shoots a bullet out of a gun
    /// </summary>
    void Shoot() 
    {
        //muzzleFlash.Play();
        RaycastHit hit;
        hit  = DetectHit();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit, range))
        {
            

            Target target = hit.transform.GetComponentInParent<Target>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForce(hit.normal * ImpachtForce);
            }

            GameObject ImpactGO = Instantiate(bulletDecal, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGO,2f);
        }
    }
    /// <summary>
    /// detects a hit with the gun
    /// </summary>
    /// <returns> return the hit of the gun</returns>
    RaycastHit DetectHit() 
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            print(hit.transform.name);
            
        }
        return hit;
    }

    public void Grenade()
    {
        Instantiate(grenades, spot.position, spot.rotation);

    }
}
