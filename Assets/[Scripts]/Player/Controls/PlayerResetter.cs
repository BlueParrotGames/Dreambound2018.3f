using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ResetPlayerPos());
    }

    IEnumerator ResetPlayerPos()
    {
        yield return new WaitForSeconds(.2f);
        foreach (Player p in WorldManager.instance.players)
        {
            p.ResetPosition();
        }
    }
}
