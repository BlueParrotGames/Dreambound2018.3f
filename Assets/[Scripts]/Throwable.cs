using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : NetThrowablesBehavior
{
    Rigidbody rb;

    [Header("Object settings")]
    public int netSpawnIndex;
    public string throwableName;

}
