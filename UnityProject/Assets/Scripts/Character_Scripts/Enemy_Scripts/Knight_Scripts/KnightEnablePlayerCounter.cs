using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnablePlayerCounter : StateMachineBehaviour
{
    private KnightEnemy _knight;

    public float counterableSeconds;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _knight = animator.GetComponent<KnightEnemy>();

        _knight.playerCounterableTrigger.SetActive(true);
        _knight.StartCoroutine(DisablePlayerCounterTrigger());
    }

    IEnumerator DisablePlayerCounterTrigger()
    {
        yield return new WaitForSeconds(counterableSeconds);
        _knight.playerCounterableTrigger.SetActive(false);
    }
}
