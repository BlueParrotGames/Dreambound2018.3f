using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool interacted = false;
    public Transform interactableTransform;

    public virtual void Interact()
    {
        Debug.LogError("Interact function not implemented, go tell a programmer!");
    }

    private void OnDrawGizmos()
    {
        if(interactableTransform == null)
        {
            interactableTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactableTransform.position, radius);
    }
}
