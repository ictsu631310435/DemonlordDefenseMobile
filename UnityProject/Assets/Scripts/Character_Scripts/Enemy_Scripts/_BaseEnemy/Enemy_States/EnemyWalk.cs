using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling Enemy's Walk State
/// </summary>
public class EnemyWalk : StateMachineBehaviour
{
    [HideInInspector] public Enemy enemy;
    
    private float _speed;

    #region Data Members
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();

        enemy.direction = Vector3.back;
        _speed = enemy.moveSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.distanceFromPlayer <= enemy.attackRange)
        {
            _speed = 0f;
            animator.SetBool("inCombat", true);
        }
        else
        {
            if (enemy.distanceFromPlayer <= enemy.lockOnRange)
            {
                enemy.LookAt(enemy.player);
            }
        }

        enemy.MoveAtSpeed(_speed);
    }
    #endregion
}
