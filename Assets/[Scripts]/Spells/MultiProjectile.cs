using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [SerializeField] int projectileCount;

    public void StartCast()
    {
        StartCoroutine("ProjectileSpawning");
    }

    IEnumerator ProjectileSpawning()
    {
        yield return new WaitForSeconds(.15f);
        for(int i = 0; i < projectileCount; i++)
        {
            yield return new WaitForSeconds(.1f);
            Instantiate(projectile, firePoint.position, Quaternion.identity);
        }
    }

}
