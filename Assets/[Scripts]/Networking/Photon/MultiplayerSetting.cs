using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dreambound.Networking
{
    public class MultiplayerSetting : MonoBehaviour
    {
        public static MultiplayerSetting instance;

        public bool delayStart;
        public int maxPlayers;

        public int menuScene;
        public int multiplayerScene;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }



    }
}

