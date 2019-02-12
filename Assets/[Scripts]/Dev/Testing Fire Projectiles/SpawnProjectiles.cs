using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject vfx;
    [SerializeField] AimAtMouse aam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SpawnVFX();
    }

    void SpawnVFX()
    {
        if (firePoint != null)
        {
            GameObject obj = Instantiate(vfx, firePoint.position, Quaternion.identity);
            if(aam != null)
            {
                obj.transform.localRotation = aam.GetRotation();
            }
        }
    }
}
