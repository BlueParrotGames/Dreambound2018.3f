using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Dreambound.Networking
{
    public class NETRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
    {
        public static NETRoom instance;
        private PhotonView PV;

        public bool isGameLoaded;
        public int currentScene;

        private Player[] players;
        public int playersInRoom;
        public int myNumberInRoom;

        public int playersInGame;

        private bool readyToCount;
        private bool readyToStart;
        public float startingTime;
        private float lessThanMaxPlayers;
        private float atMaxPlayers;
        private float timeToStart;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
            SceneManager.sceneLoaded -= OnSceneFinishedLoading;
        }

        public override void OnJoinedRoom()
        {
            // Player Enters the scene, whether that's as host or as non-hosting client
            
            base.OnJoinedRoom();
            Debug.Log("Joined a random room");
            players = PhotonNetwork.PlayerList;
            playersInRoom = players.Length;
            myNumberInRoom = playersInRoom;

            PhotonNetwork.NickName = "DEV " + myNumberInRoom.ToString();

            if(MultiplayerSetting.instance.delayStart)
            {
                Debug.Log(playersInRoom + " out of " + MultiplayerSetting.instance.maxPlayers + " players in room");
                if(playersInRoom > 1)
                {
                    readyToCount = true;
                }
                if (playersInRoom == MultiplayerSetting.instance.maxPlayers)
                {
                    readyToStart = true;
                    if (!PhotonNetwork.IsMasterClient)
                        return;

                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }
            }
            else
            {
                StartGame();
            }
        }

        void Start()
        {
            PV = GetComponent<PhotonView>();
            readyToCount = false;
            readyToStart = false;

            lessThanMaxPlayers = startingTime;
            atMaxPlayers = 6;
            timeToStart = startingTime;

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("New player joined");
            players = PhotonNetwork.PlayerList;
            playersInRoom++;

            if(MultiplayerSetting.instance.delayStart)
            {
                Debug.Log(playersInRoom + " out of " + MultiplayerSetting.instance.maxPlayers + " players in room");
                if(playersInRoom > 1)
                {
                    readyToCount = true;

                }
                if(playersInRoom == MultiplayerSetting.instance.maxPlayers)
                {
                    readyToStart = true;
                }

                if (!PhotonNetwork.IsMasterClient)
                    return;

                PhotonNetwork.CurrentRoom.IsOpen = false;

            }
        }

        private void Update()
        {
            string p = PhotonNetwork.NetworkClientState.ToString();
            Debug.Log(p);

            if (MultiplayerSetting.instance.delayStart)
            {
                if(playersInRoom == 1)
                {
                    RestartTimer();
                }
                if(!isGameLoaded)
                {
                    if(readyToStart)
                    {
                        atMaxPlayers -= Time.deltaTime;
                        lessThanMaxPlayers = atMaxPlayers;
                        timeToStart = atMaxPlayers;
                    }

                    else if(readyToCount)
                    {
                        lessThanMaxPlayers -= Time.deltaTime;
                        timeToStart = lessThanMaxPlayers;
                    }
                    Debug.Log("Time Till Starting: " + timeToStart);
                    if(timeToStart <= 0)
                    {
                        StartGame();
                    }
                }
            }
        }

        void RestartTimer()
        {
            lessThanMaxPlayers = startingTime;
            timeToStart = startingTime;
            atMaxPlayers = 6;

            readyToCount = false;
            readyToStart = false;
        }

        void StartGame()
        {
            isGameLoaded = true;
            if (!PhotonNetwork.IsMasterClient)
                return;

            if(MultiplayerSetting.instance.delayStart)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            PhotonNetwork.LoadLevel(MultiplayerSetting.instance.multiplayerScene);
        }

        void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            currentScene = scene.buildIndex;
            if(currentScene == MultiplayerSetting.instance.multiplayerScene)
            {
                isGameLoaded = true;

                if(MultiplayerSetting.instance.delayStart)
                {
                    PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
                }
                else
                {
                    RPC_CreatePlayer();
                }
            }
        }

        [PunRPC]
        void RPC_LoadedGameScene()
        {
            playersInGame++;
            if(playersInGame == PhotonNetwork.PlayerList.Length)
            {
                PV.RPC("RPC_CreatePlayer", RpcTarget.All);
            }
        }

        [PunRPC]
        void RPC_CreatePlayer()
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerPhoton"), Vector3.one, Quaternion.identity, 0);
        }
    }
}