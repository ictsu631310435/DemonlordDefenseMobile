using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling Enemy's Combat State
/// </summary>
public class EnemyCombat : StateMachineBehaviour
{
    [HideInInspector] public Enemy enemy;

    #region Data Members
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();

        enemy.rigidBody.velocity = Vector3.zero;

        enemy.nextAttackTime = Time.time + enemy.secondsBtwnAttacks;

        if (enemy.animator)
        {
            enemy.animator.SetTrigger("Combat");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time < enemy.nextAttackTime)
        {
            enemy.LookAt(enemy.player);
        }
        else
        {
            enemy.nextAttackTime = Time.time + enemy.secondsBtwnAttacks;

            animator.SetTrigger("Attack");
        }
    }
    #endregion
}
