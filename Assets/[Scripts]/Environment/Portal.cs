using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if(p != null)
        {
            Debug.Log("Player entered the portal!");
            SceneManager.LoadScene("Dungeon");
        }
    }
}
