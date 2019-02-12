using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    Animator animator;

    [Header("Settings")]
    [SerializeField] bool ikActive = false;

    [Header("Use")]
    [SerializeField] bool useLook = true;
    [SerializeField] bool useHands = false;

    [Space]
    [SerializeField] Transform rightHandTarget = null;
    [SerializeField] Transform leftHandTarget = null;
    [SerializeField] Transform lookTarget = null;

    [SerializeField] Vector3 handOffset;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                if (lookTarget != null && useLook)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookTarget.position);
                }

                if (rightHandTarget != null && useHands)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position + handOffset);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
                }

                if (leftHandTarget != null && useHands)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position + handOffset);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}