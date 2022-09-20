using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float forceAmount = 10f;

    public GameObject explosion;

    float countdown;
    bool hasExploded = false;
    RaycastHit hit;
    public LayerMask enemy;
    [SerializeField] TitanfallMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<TitanfallMovement>();//used to get the player momentum vector to also aplay it to the grenade
        GetComponent<Rigidbody>().AddForce((player.playerCamera.transform.forward * forceAmount) + (player.move * 0.75f) , ForceMode.Impulse);
        countdown = delay; //grenade timmer
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; // makes grenade go off
        if (countdown <= 0f && hasExploded == false) 
        {
            Explode();
            hasExploded = true;
        }
    }
    /// <summary>
    /// makes the grenade explode
    /// </summary>
    void Explode() 
    {

        //Debug.Log("BOOM");

        //show effect
        explosion.SetActive(true);
        explosion.transform.position = transform.position;

        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders) 
        {
            Debug.Log(nearbyObject);
            //ray cast to see if the object is able to be ray casted to in the sphere
            if (Physics.Raycast(transform.position,(nearbyObject.transform.position - transform.position).normalized,out hit,radius,enemy)) 
            {
                //the ragdoll controller so you can get al the conponents an make them move witha grenade
                RagdollController rc;
                rc = hit.transform.GetComponentInParent<RagdollController>();

                rc.BeRagdoll();//get a controller for the ragdoll to get all the components 
                rc.health =0;//kills the player

                Rigidbody[] limbs = rc.GetComponentsInChildren<Rigidbody>();//a array of all the limbs
                for (int i = 0; i < limbs.Length; i++)
                {
                    limbs[i].AddExplosionForce(800f, transform.position, radius * 2);//adds force to all the limbs
                }
                

            }
        }


        //gets rid of the grenade
        Destroy(this.gameObject);
    }
}
