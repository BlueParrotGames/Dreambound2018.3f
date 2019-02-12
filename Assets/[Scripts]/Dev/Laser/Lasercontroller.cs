using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasercontroller : MonoBehaviour
{
    [SerializeField] GameObject firePoint;
    [SerializeField] LineRenderer lr;
    [SerializeField] float maxLength;

    RaycastHit hit;

    void Update()
    {
        lr.SetPosition(0, firePoint.transform.position);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        transform.position = firePoint.transform.position;

        if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, maxLength))
        {
            if(hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            Vector3 pos = mouseRay.GetPoint(maxLength);
            lr.SetPosition(1, pos);
        }
    }
}
