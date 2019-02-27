using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Dreambound.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(NetworkAnimator))]
public class CharacterMovement : DreamboundNetBehavior
{
    CharacterController characterController;
    Animator anim;
    //PhotonView PV;
    //NetworkAnimator nAnim;

    private Vector3 dir;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotSpeed = 2f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float gravity = 20.0f;

    [SerializeField] GameObject playerCam;

    float dist;


    void Start()
    {
        //PV = GetComponent<PhotonView>();

        //if (PV.IsMine)
        //    playerCam.SetActive(true);
        //else
        //    playerCam.SetActive(false);

        //nAnim = GetComponent<NetworkAnimator>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gravity = (Physics.gravity.y * 2) * -1;

        //for (int i = 0; i < anim.parameterCount; i++)
        //{
        //    nAnim.SetParameterAutoSend(i, true);
        //}
    }

    void Update()
    {
        if (true)
        {
            if (characterController.isGrounded)
            {
                dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                dir = transform.TransformDirection(dir);
                dir *= moveSpeed;
                if (Input.GetButton("Jump"))
                {
                    anim.SetTrigger("Jump");

                    dir.y = jumpHeight;
                }
            }

            if (Input.GetAxisRaw("Rotator") < 0)
                transform.Rotate(Vector3.down * Time.deltaTime * (rotSpeed * 100));

            if (Input.GetAxisRaw("Rotator") > 0)
                transform.Rotate(Vector3.up * Time.deltaTime * (rotSpeed * 100));

            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));

            dir.y -= gravity * Time.deltaTime;

            characterController.Move(dir * Time.deltaTime);

            float scrollValue = Input.GetAxis("Mouse ScrollWheel");            

            if (scrollValue != 0)
            {
                Debug.Log(dist);

                if (dist < 0.1 && dist > -2.1)
                {
                    dist += Input.GetAxis("Mouse ScrollWheel");
                    float R = scrollValue * 15;
                    float PosX = Camera.main.transform.eulerAngles.x + 90;
                    float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);
                    PosX = PosX / 180 * Mathf.PI;
                    PosY = PosY / 180 * Mathf.PI;
                    float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);
                    float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);
                    float Y = R * Mathf.Cos(PosX);
                    float CamX = Camera.main.transform.position.x;
                    float CamY = Camera.main.transform.position.y;
                    float CamZ = Camera.main.transform.position.z;
                    Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);
                }
            }
        }
    }
}