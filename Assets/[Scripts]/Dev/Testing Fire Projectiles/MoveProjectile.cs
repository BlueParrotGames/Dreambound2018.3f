using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] float speed;

    private void Start()
    {
        StartCoroutine(WaitForDestroy());
    }

    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit something!");

        speed = 0;
        //Destroy(gameObject);
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
