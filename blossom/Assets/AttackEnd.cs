using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnd : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackSystem sys = animator.transform.GetChild(0).GetComponent<AttackSystem>();
        if (stateInfo.IsTag("0"))
        {
            sys.currentAttack = AttackSystem.AttackType.NONE;
        }
        else if (stateInfo.IsTag("1"))
        {
            sys.currentAttack = AttackSystem.AttackType.ATTACK_SLAP;
        }
        else if (stateInfo.IsTag("2"))
        {
            sys.currentAttack = AttackSystem.AttackType.ATTACK_FIST;
        }
        else if (stateInfo.IsTag("3"))
        {
            sys.currentAttack = AttackSystem.AttackType.ATTACK_RAGE;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("Attack1", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
