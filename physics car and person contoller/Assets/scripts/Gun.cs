using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float ImpachtForce = 30f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject bulletDecal;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot() 
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit, range))
        {
            print(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
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
}
