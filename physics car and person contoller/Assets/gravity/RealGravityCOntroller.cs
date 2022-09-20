using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealGravityCOntroller : MonoBehaviour
{
    public static List<RealGravity> RealGravityObjects = new List<RealGravity>();
    public float GravitationConstant = 6.67438f * Mathf.Pow(10, -11);
    [Range(1, 2000000)] public float GravityValueMultiplyer;

    private void FixedUpdate()
    {
        int c = RealGravityObjects.Count;
        for (int i = 0; i < c - 1; i++) 
        {
            
            for (int j = 1; j < c - i; j++)
            {
                ApplyRealGravity(RealGravityObjects[i], RealGravityObjects[j + i]);
            }
        }
    }

    void ApplyRealGravity(RealGravity A, RealGravity B) 
    {
        Rigidbody m1 = A.gameObject.GetComponent<Rigidbody>();
        Rigidbody m2 = B.gameObject.GetComponent<Rigidbody>();

        float f = GravitationConstant * (m1.mass * m2.mass) / Vector3.Distance(A.transform.position, B.transform.position);
        Vector3 dir = (B.transform.position - A.transform.position).normalized;

        f *= GravityValueMultiplyer;

        m1.AddForce(f * dir);

        m2.AddForce(-f * dir);
        //Debug.Log(f);
    }
    static void OnDestroy()
    {
        RealGravityObjects.Clear();
    }
}
