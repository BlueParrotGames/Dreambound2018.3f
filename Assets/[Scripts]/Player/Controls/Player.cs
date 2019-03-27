using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
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
    [SerializeField] bool useLowCamera;
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
    [SerializeField] CinemachineVirtualCamera freeLook;

    RaycastHit hit;
    Ray ray;


    private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
    private Quaternion leftFootIkRotation, rightFootIkRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

    [Header("Feet Grounder")]
    public bool enableFeetIk = true;
    [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0, 1)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] private float feetToIkPositionSpeed = 0.5f;

    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";

    public bool useProIkFeature = false;
    public bool showSolverDebug = true;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f; //InputX dampening
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f; //InputZ dampening
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f; //dampens the time of starting the player after input is pressed
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f; //dampens the time of stopping the player after release of input

    private float verticalVel; //Vertical velocity -- currently work in progress
    private Vector3 moveVector; //movement vector -- currently work in progres

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
        ikControl = GetComponent<IKControl>();
        anim = GetComponent<Animator>();

        gravity = (Physics.gravity.y * 2) * -1;
        freeLook = GetComponentInChildren<CinemachineVirtualCamera>();
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
            freeLook.enabled = true;
            networkObject.SendRpc(RPC_SET_PLAYER_NAME, Receivers.AllBuffered, PlayerPrefs.GetString("PlayerName"));
            networkObject.SendRpc(RPC_SUBSCRIBE_TO_WORLD_MANAGER, Receivers.AllBuffered);
        }
        else
        {
            playerCam.enabled = false;
            freeLook.enabled = false;
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
                    enableFeetIk = false;
                    networkObject.SendRpc(RPC_SEND_ANIM_TRIGGER, Receivers.All, "Jump");
                    dir.y = jumpHeight;
                }
                enableFeetIk = true;
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
                #region --------------------Interaction

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
                #endregion
            }

            #region --------------------Camera
            if (useLowCamera)
            {
                freeLook.enabled = false;
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
            }
            else if(!useLowCamera)
            {
                playerCam.transform.parent = null;
                freeLook.Follow = transform;
                freeLook.LookAt = transform;
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

        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }

        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        //find and raycast to the ground to find positions
        FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation); // handle the solver for right foot
        FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation); //handle the solver for the left foot

        playerOverhead.transform.LookAt(freeLook.transform);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }

        MovePelvisHeight();

        //right foot ik position and rotation -- utilise the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }

        MoveFeetToIkPoint(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPositionY);

        //left foot ik position and rotation -- utilise the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }

        MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPositionY);
    }


    /// <summary>
    /// Moves the feet to ik point.
    /// </summary>
    /// <param name="foot">Foot.</param>
    /// <param name="positionIkHolder">Position ik holder.</param>
    /// <param name="rotationIkHolder">Rotation ik holder.</param>
    /// <param name="lastFootPositionY">Last foot position y.</param>
    void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
    {
        Vector3 targetIkPosition = anim.GetIKPosition(foot);

        if (positionIkHolder != Vector3.zero)
        {
            targetIkPosition = transform.InverseTransformPoint(targetIkPosition);
            positionIkHolder = transform.InverseTransformPoint(positionIkHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, feetToIkPositionSpeed);
            targetIkPosition.y += yVariable;

            lastFootPositionY = yVariable;

            targetIkPosition = transform.TransformPoint(targetIkPosition);

            anim.SetIKRotation(foot, rotationIkHolder);
        }

        anim.SetIKPosition(foot, targetIkPosition);
    }
    /// <summary>
    /// Moves the height of the pelvis.
    /// </summary>
    private void MovePelvisHeight()
    {

        if (rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastPelvisPositionY == 0)
        {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }

        float lOffsetPosition = leftFootIkPosition.y - transform.position.y;
        float rOffsetPosition = rightFootIkPosition.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

        Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;

        newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);

        anim.bodyPosition = newPelvisPosition;

        lastPelvisPositionY = anim.bodyPosition.y;
    }

    /// <summary>
    /// We are locating the Feet position via a Raycast and then Solving
    /// </summary>
    /// <param name="fromSkyPosition">From sky position.</param>
    /// <param name="feetIkPositions">Feet ik positions.</param>
    /// <param name="feetIkRotations">Feet ik rotations.</param>
    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIkPositions, ref Quaternion feetIkRotations)
    {
        //raycast handling section 
        RaycastHit feetOutHit;

        if (showSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);

        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundRaycast, environmentLayer))
        {
            //finding our feet ik positions from the sky position
            feetIkPositions = fromSkyPosition;
            feetIkPositions.y = feetOutHit.point.y + pelvisOffset;
            feetIkRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;

            return;
        }

        feetIkPositions = Vector3.zero; //it didn't work :(

    }
    /// <summary>
    /// Adjusts the feet target.
    /// </summary>
    /// <param name="feetPositions">Feet positions.</param>
    /// <param name="foot">Foot.</param>
    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundRaycast;

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