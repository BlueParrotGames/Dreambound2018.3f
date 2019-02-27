using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


namespace Dreambound.Networking
{
    public class NETPlayer : MonoBehaviour
    {
        PhotonView PV;
        Camera cam;

        [SerializeField] string ID;
        [SerializeField] GameObject UIBillboard;
        [SerializeField] Text playerName;

        void Start()
        {
            PV = GetComponent<PhotonView>();
            ID = PhotonNetwork.NickName;
            cam = GetComponentInChildren<Camera>();
            playerName.text = ID;


            //int spawnPicker = Random.Range(0, NETSetup.instance.spawnPoints.Length);
            //if(PV.IsMine)
            //{
            //    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerPhoton"), NETSetup.instance.spawnPoints[spawnPicker].position, NETSetup.instance.spawnPoints[spawnPicker].rotation, 0);
            //}

        }

        private void FixedUpdate()
        {
            UIBillboard.transform.LookAt(cam.transform);

        }
    }
}
