using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RagdollCelaner : MonoBehaviour
{
    [ContextMenu("destroy ragdoll")]
    void CleanRagdoll() 
    {
        Transform [] limbs = GetComponentsInChildren<Transform>();
        for (int i = 0; i < limbs.Length; i++)
        {
            Rigidbody rb;
            CharacterJoint joint;
            Collider col;
            limbs[i].TryGetComponent<Rigidbody>(out rb);
            limbs[i].TryGetComponent<CharacterJoint>(out joint);
            limbs[i].TryGetComponent<Collider>(out col);
            DestroyImmediate(joint,true);
            DestroyImmediate(rb,true);
            DestroyImmediate(col,true);
           
        }
    }
}
