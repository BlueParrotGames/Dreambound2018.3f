using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatieMovement : MonoBehaviour
{
    private void Start()
    {
        transform.LookAt(Camera.main.transform.position);

        StartCoroutine(WaitForDestroy());
    }
    void Update()
    {
        transform.position += new Vector3(0, 2, 0) * Time.deltaTime;
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
