using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;

public class GameLogic : DreamboundNetBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Instance.InstantiateDreamboundNet();
    }


}
