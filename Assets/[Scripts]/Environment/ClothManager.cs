using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Cloth))]
[RequireComponent(typeof(SphereCollider))]
public class ClothManager : MonoBehaviour
{
    Cloth cloth;
    SphereCollider coll;
    List<CapsuleCollider> capColliders = new List<CapsuleCollider>();

    void Start()
    {
        cloth = GetComponent<Cloth>();
        coll = GetComponent<SphereCollider>();
        coll.isTrigger = true;

        InvokeRepeating("UpdateClothColliders", 0, 0.25f);
    }

    void UpdateClothColliders()
    {
        cloth.capsuleColliders = capColliders.ToArray();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CapsuleCollider>())
        {
            CapsuleCollider coll = other.GetComponent<CapsuleCollider>();
            capColliders.Add(coll);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CapsuleCollider>())
        {
            CapsuleCollider coll = other.GetComponent<CapsuleCollider>();
            capColliders.Remove(coll);
        }
    }
}