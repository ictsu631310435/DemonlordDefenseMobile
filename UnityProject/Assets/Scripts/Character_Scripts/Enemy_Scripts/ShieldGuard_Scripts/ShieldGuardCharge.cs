using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardCharge : StateMachineBehaviour
{
    private ShieldGuardEnemy _shieldGuard;

    [SerializeField] private float _remainingDuration;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard = animator.GetComponent<ShieldGuardEnemy>();

        /*Vector3 heading = _shieldGuard.player.position - _shieldGuard.transform.position;
        Vector3 direction = heading.normalized;

        _shieldGuard.rigidBody.velocity = direction * _shieldGuard.moveSpeed;*/

        //_remainingDuration = _shieldGuard.chargeDuration;

        _shieldGuard.chargeHitBox.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (/*_remainingDuration > 0.0f &&*/ !_shieldGuard.hitSomething)
        {
            //_remainingDuration -= Time.deltaTime;

            Vector3 heading = _shieldGuard.player.position - _shieldGuard.transform.position;
            Vector3 direction = heading.normalized;

            _shieldGuard.rigidBody.velocity = direction * _shieldGuard.moveSpeed;
        }
        else
        {
            switch (_shieldGuard.whatsHit)
            {
                case ShieldGuardEnemy.WhatsHit.Player:
                    animator.SetBool("isPushBack", true);
                    break;

                case ShieldGuardEnemy.WhatsHit.Obstacle:
                    animator.SetBool("isStun", true);
                    break;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shieldGuard.rigidBody.velocity = Vector3.zero;
        _shieldGuard.chargeHitBox.SetActive(false);
        _shieldGuard.hitSomething = false;
        _shieldGuard.whatsHit = ShieldGuardEnemy.WhatsHit.Nothing;

        //_remainingDuration = 0.0f;
    }
}
