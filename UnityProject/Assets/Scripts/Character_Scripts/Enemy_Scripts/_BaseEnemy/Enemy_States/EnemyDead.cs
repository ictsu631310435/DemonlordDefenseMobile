using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling Enemy's Dead State
/// </summary>
public class EnemyDead : StateMachineBehaviour
{
    public float destroyDelaySeconds;

    [HideInInspector] public Enemy enemy;

    #region Data Members
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, destroyDelaySeconds);

        enemy = animator.GetComponent<Enemy>();
        enemy.CancelInvoke();
        enemy.speed = -0.05f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.MoveAtSpeed(0f);
    }
    #endregion
}
