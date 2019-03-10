using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

[RequireComponent(typeof(CharacterController))]
public class Player : PlayerBehavior
{
    public enum CombatState { IDLE, COMBAT }
    public CombatState combatState;

    CharacterController characterController;
    Animator anim;

    [SerializeField] Vector3 mousePos;
    private Vector3 dir;
    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotSpeed = 2f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] Vector2 scrollSpeed = new Vector2(2, 1);
    public LoginInfo playerInfo;

    [Header("Rig")]
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] IKControl ikControl;
    
    [Header("Objects")]
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject playerOverhead;
    [SerializeField] TMPro.TMP_Text playerNameField;

    [Header("Player Items")]
    [SerializeField] ParabolicShoot throwable;
    //Interactable interactable;
    public CapsuleCollider clothCollider;


    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
        ikControl = GetComponent<IKControl>();
        anim = GetComponent<Animator>();

        gravity = (Physics.gravity.y * 2) * -1;
        playerOverhead.transform.LookAt(playerCam.transform);
    }

    public void ResetPosition()
    {
        Debug.Log("Resetting position!");
        transform.position = Vector3.one;
        //networkObject.position = Vector3.one;
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (networkObject.IsOwner)
        {
            playerCam.enabled = true;
            networkObject.SendRpc(RPC_SET_PLAYER_NAME, Receivers.AllBuffered, PlayerPrefs.GetString("PlayerName"));
            networkObject.SendRpc(RPC_SUBSCRIBE_TO_WORLD_MANAGER, Receivers.AllBuffered);
        }
        else
        {
            playerCam.enabled = false;
        }
    }

    void Update()
    {
        //Debug.Log(transform.position);
        if (networkObject.IsOwner)
        {
            #region --------------------Movement
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

            #endregion

            //Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray, Vector3.down,);

            if (combatState == CombatState.COMBAT)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    networkObject.SendRpc(RPC_SEND_ANIM_TRIGGER, Receivers.All, "Slash1");
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    networkObject.SendRpc(RPC_SEND_ANIM_TRIGGER, Receivers.All, "Throw");
                }
                //RemoveFocus();
            }
            else
            {

                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        //interactable = hit.collider.GetComponent<Interactable>();
                        //if (interactable != null)
                        //{
                        //    if (Vector3.Distance(transform.position, interactable.interactableTransform.position) <= interactable.radius)
                        //    {
                        //        SetFocus();
                        //    }
                        //}
                    }
                }

                //if(interactable != null)
                //{
                //    if (Vector3.Distance(transform.position, interactable.interactableTransform.position) >= interactable.radius)
                //    {
                //        RemoveFocus();
                //    }
                //}
            }

            #region --------------------Scrolling
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

            #endregion

            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
            anim.SetInteger("animState", (int)combatState);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), (float)combatState, 3f * Time.deltaTime));
        }
    }

    private void FixedUpdate()
    {
        if(networkObject.IsOwner)
        {
            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;

            networkObject.animhor = anim.GetFloat("Horizontal");
            networkObject.animvert = anim.GetFloat("Vertical");

            networkObject.animstate = (int)combatState;
        }
        else if(!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;

            anim.SetFloat("Horizontal", networkObject.animhor);
            anim.SetFloat("Vertical", networkObject.animvert);

            combatState = (CombatState)networkObject.animstate;
            anim.SetInteger("animState", networkObject.animstate);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), (float)networkObject.animstate, 3f * Time.deltaTime));
            return;
        }

        if(transform.position.y <= -100)
        {
            transform.position = Vector3.one;
        }
    }

    void SetFocus()
    {
        //interactable.interacted = false;
        //ikControl.FocusOn(interactable.transform);
        //interactable.Interact();
        Debug.Log("Interacting bitch");
    }

    void RemoveFocus()
    {
        //interactable.interacted = false;
        //interactable = null;
        ikControl.ResetFocus();
        Debug.Log("No longer interacting");
    }

    public void Launch()
    {
        if (networkObject.IsOwner)
        {
            throwable = (ParabolicShoot)NetworkManager.Instance.InstantiateNetThrowables(throwable.netSpawnIndex, rightHand.position, Quaternion.identity, true);

            Vector3 targetPos = new Vector3();
            ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
            }

            Debug.Log("Throw time!");
            throwable.networkStarted += (NetworkBehavior behavior) =>
            {
                throwable.networkObject.SendRpc(NetThrowablesBehavior.RPC_NETWORK_LAUNCH, Receivers.All, targetPos);
            };
        }
    }

    public void TriggerCombat()
    {
        if(networkObject.IsOwner)
            combatState = (CombatState)1;
    }

    public void AbandonCombat()
    {
        // temporary function
        if (networkObject.IsOwner)
            combatState = (CombatState)0;
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

    public override void SubscribeToWorldManager(RpcArgs args)
    {
        WorldManager.instance.players.Add(this);
    }
}