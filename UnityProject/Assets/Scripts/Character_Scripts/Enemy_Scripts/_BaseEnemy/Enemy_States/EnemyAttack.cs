using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling Enemy's Attack State
/// </summary>
public class EnemyAttack : StateMachineBehaviour
{
    [Tooltip("Delay time before calling Attack method")]
    public float damageDelaySeconds;

    [HideInInspector] public Enemy enemy;

    #region Unity Callbacks
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();

        enemy.Invoke("Attack", damageDelaySeconds);

        if (enemy.animator)
        {
            enemy.animator.SetTrigger("Attack");
        }
    }
    #endregion
}
