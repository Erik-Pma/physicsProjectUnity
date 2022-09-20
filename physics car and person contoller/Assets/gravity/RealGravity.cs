using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RealGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RealGravityCOntroller.RealGravityObjects.Add(this);
    }

    
}
