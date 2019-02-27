using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreambound.Networking
{
    public class NETSetup : MonoBehaviour
    {
        public static NETSetup instance;

        public Transform[] spawnPoints;

        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}

