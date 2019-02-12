using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float maxLength;

    Ray rayMouse;
    Vector3 pos;
    Vector3 dir;
    Quaternion rot;
    RaycastHit hit;


    void Update()
    {
        if(cam != null)
        {
            rayMouse = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
            {
                dir = hit.point - transform.position;
                rot = Quaternion.LookRotation(dir);
                transform.localRotation = Quaternion.Lerp(transform.rotation, rot, 1);
            }
            else
            {
                Vector3 pos = rayMouse.GetPoint(maxLength);
                rot = Quaternion.LookRotation(pos);
                transform.localRotation = Quaternion.Lerp(transform.rotation, rot, 1);
            }
        }
    }

    public Quaternion GetRotation()
    {
        return rot;
    }
}
