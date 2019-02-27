using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimController : MonoBehaviour
{
    public enum CombatState { COMBAT, IDLE }
    public CombatState combatState;

    private Animator anim;
    private Vector2 movement;

    [SerializeField] MultiProjectile castHand;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch(combatState)
        {
            case CombatState.COMBAT:
                {
                    anim.SetBool("Idle", false);
                    anim.SetBool("Combat", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        anim.SetTrigger("Slash1");
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        anim.SetTrigger("Cast1");
                        castHand.StartCast();
                    }

                    break;
                }
            case CombatState.IDLE:
                {
                    anim.SetBool("Combat", false);
                    anim.SetBool("Idle", true);
                    break;
                }
        }


    }
}
