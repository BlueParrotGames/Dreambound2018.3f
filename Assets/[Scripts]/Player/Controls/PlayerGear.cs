using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGear : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer playerRenderer;
    [SerializeField] SkinnedMeshRenderer[] gear;

    void Start()
    {

    }

    void Update()
    {
        foreach(SkinnedMeshRenderer smr in gear)
        {
            smr.bones = playerRenderer.bones;
        }
    }
}
