using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }
    /// <summary>
    /// kills the target when called
    /// </summary>
    void Die() 
    {
        Destroy(gameObject); //detroys the target
    }
}
