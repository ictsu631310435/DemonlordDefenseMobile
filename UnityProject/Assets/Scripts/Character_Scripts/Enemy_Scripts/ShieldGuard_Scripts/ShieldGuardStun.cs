using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardStun : StateMachineBehaviour
{
    public float stunDuration;

    private float _remainingDuration;

    private ShieldGuardEnemy _shieldGuard;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard = animator.GetComponent<ShieldGuardEnemy>();

        _remainingDuration = stunDuration;

        _shieldGuard.animator.SetBool("isStun", true);

        _shieldGuard.stunIndicator.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_remainingDuration > 0.0f)
        {
            _remainingDuration -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("isStun", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard.animator.SetBool("isStun", false);

        _shieldGuard.stunIndicator.SetActive(false);
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
