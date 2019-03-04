using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class Throwable : NetThrowablesBehavior
{
    [Header("Object settings")]
    public int netSpawnIndex;
    public string throwableName;

    private void Update()
    {
        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }

}
