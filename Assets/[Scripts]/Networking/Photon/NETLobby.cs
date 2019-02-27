using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Dreambound.Networking
{
    public class NETLobby : MonoBehaviourPunCallbacks
    {
        public static NETLobby instance;

        public GameObject quickJoinButton;
        public GameObject cancelButton;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master server");
            PhotonNetwork.AutomaticallySyncScene = true;
            quickJoinButton.SetActive(true);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("No rooms available.");
            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Probably already a room with the same name");
            CreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined a random room");
        }

        void CreateRoom()
        {
            int roomID = Random.Range(0, 1000);
            RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.instance.maxPlayers };
            PhotonNetwork.CreateRoom("Room" + roomID, roomOptions);
            Debug.Log("Created a new room, ID: " + roomID);
        }

        public void OnBattleButtonClicked()
        {
            quickJoinButton.SetActive(false);
            cancelButton.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
        }

        public void OnCancelButtonClicked()
        {
            cancelButton.SetActive(false);
            quickJoinButton.SetActive(true);

            PhotonNetwork.LeaveRoom();
        }
    }
}