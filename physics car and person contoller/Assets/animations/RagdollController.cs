using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    Rigidbody[] limbs;
    public Animator animator;
    public int health = 10;
    [ReadOnly]
    public bool isRagdoll = false;
    // Start is called before the first frame update
    void Start()
    {
        limbs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        for (int x = 0; x < limbs.Length; x++) 
        {
            limbs[x].isKinematic = true;
        }
    }
    [ContextMenu("ragdoll")]
    private void Update()
    {
        
        if (health < 1 && !isRagdoll) 
        {
            BeRagdoll();
            
        }
    }
    /// <summary>
    /// makes the charter ragdoll on the 
    /// </summary>
    public void BeRagdoll() 
    {
        isRagdoll = true;
        animator.enabled = false;
        for (int x = 0; x < limbs.Length; x++)
        {
            limbs[x].isKinematic = false;
        }
    }
   
}
