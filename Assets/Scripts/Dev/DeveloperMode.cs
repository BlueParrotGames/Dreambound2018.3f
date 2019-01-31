using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMode : MonoBehaviour
{
    [Range(0,1)][SerializeField] float timeScale = 1;

    void Update()
    {
        Time.timeScale = timeScale;
    }
}
