using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayer : MonoBehaviour
{
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "floor")
        {
            print("off the platform");
            gameObject.GetComponentInParent<TitanfallMovement>().transform.parent = null;
        }
    }
}
