using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPushBack : StateMachineBehaviour
{
    private ShieldGuardEnemy _shieldGuard;

    [SerializeField] private float _remainingDuration;
    private Vector3 _direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard = animator.GetComponent<ShieldGuardEnemy>();

        Vector3 heading = _shieldGuard.player.position - _shieldGuard.transform.position;
        _direction = -heading.normalized;

        _remainingDuration = _shieldGuard.pushBackDuration;

        _shieldGuard.animator.SetBool("isPushBack", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_remainingDuration > 0.0f)
        {
            _remainingDuration -= Time.deltaTime;

            _shieldGuard.rigidBody.velocity = _direction * _shieldGuard.pushBackSpeed;
        }
        else
        {
            animator.SetBool("isPushBack", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard.animator.SetBool("isPushBack", false);

        _shieldGuard.rigidBody.velocity = Vector3.zero;

        _remainingDuration = 0.0f;
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
