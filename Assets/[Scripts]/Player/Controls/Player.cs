﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

[RequireComponent(typeof(CharacterController))]
public class Player : PlayerBehavior
{
    CharacterController characterController;
    Animator anim;

    private Vector3 dir;
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotSpeed = 2f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] Vector2 scrollSpeed = new Vector2(2, 1);
    public LoginInfo playerInfo;


    [Header("Objects")]
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject playerOverhead;
    [SerializeField] TMPro.TMP_Text playerNameField;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gravity = (Physics.gravity.y * 2) * -1;

        playerOverhead.transform.LookAt(playerCam.transform);
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (networkObject.IsOwner)
        {
            playerCam.GetComponent<Camera>().enabled = true;
            networkObject.SendRpc(RPC_SET_PLAYER_NAME, Receivers.AllBuffered, PlayerPrefs.GetString("PlayerName"));
        }
        else
        {
            playerCam.GetComponent<Camera>().enabled = false;
        }
    }

    void Update()
    {
        if (networkObject.IsOwner)
        {
            if (characterController.isGrounded)
            {
                dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                dir = transform.TransformDirection(dir);
                dir *= moveSpeed;
                if (Input.GetButton("Jump"))
                {
                    networkObject.SendRpc(RPC_SEND_ANIM_TRIGGER, Receivers.All, "Jump");
                    dir.y = jumpHeight;
                }
            }

            if (Input.GetAxisRaw("Rotator") < 0)
                transform.Rotate(Vector3.down * Time.deltaTime * (rotSpeed * 100));

            if (Input.GetAxisRaw("Rotator") > 0)
                transform.Rotate(Vector3.up * Time.deltaTime * (rotSpeed * 100));

            dir.y -= gravity * Time.deltaTime;
            characterController.Move(dir * Time.deltaTime);

            if(Input.GetMouseButtonUp(0))
            {
                networkObject.SendRpc(RPC_SEND_ANIM_TRIGGER, Receivers.All, "Slash1");
            }

            float scrollValue = Input.GetAxis("Mouse ScrollWheel");
            if (scrollValue != 0)
            {
                playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x,
                                                            playerCam.transform.localPosition.y - (scrollSpeed.y * scrollValue),
                                                            playerCam.transform.localPosition.z + (scrollSpeed.x * scrollValue));

                playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x,
                                                Mathf.Clamp(playerCam.transform.localPosition.y, 2.1f, 3.8f),
                                                Mathf.Clamp(playerCam.transform.localPosition.z, -5.5f, -2.1f));
            }

            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));

            #region Network Assign
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;

            networkObject.animhor = anim.GetFloat("Horizontal");
            networkObject.animvert = anim.GetFloat("Vertical");
            #endregion
        }
        else if (!networkObject.IsOwner)
        {

            // Gathering position and rotation from the network
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;

            anim.SetFloat("Horizontal", networkObject.animhor);
            anim.SetFloat("Vertical", networkObject.animvert);
            return;
        }
    }

    public override void SetPlayerName(RpcArgs args)
    {
        string name = args.GetNext<string>();
        playerNameField.text = name;
    }

    public override void SendAnimTrigger(RpcArgs args)
    {
        string triggerID = args.GetNext<string>();
        anim.SetTrigger(triggerID);
    }
}