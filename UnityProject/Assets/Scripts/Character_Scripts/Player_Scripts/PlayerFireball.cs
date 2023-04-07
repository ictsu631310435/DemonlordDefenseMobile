using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : StateMachineBehaviour
{
    private PlayerController _playerControl;
    private PlayerInputReceiver _playerInput;

    public float fireDelaySeconds;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerControl = animator.GetComponent<PlayerController>();
        _playerInput = animator.GetComponent<PlayerInputReceiver>();

        _playerControl.enableRegen = false;

        //_playerControl.Invoke("ShootFireball", fireDelaySeconds);
        _playerControl.animator.SetTrigger("Fireball");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_playerInput.fireball)
        {
            animator.SetBool("ChargingFireball", false);
        }
        else
        {
            animator.SetBool("ChargingFireball", true);
            _playerControl.animator.SetBool("ChargingFireball", true);
        }

        if (_playerControl.canRotate)
        {
            animator.transform.rotation = _playerInput.lookRotation;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerControl.ShootFireball();
        //_playerInput.fireball = false;

        _playerControl.enableRegen = true;

        _playerControl.animator.SetBool("ChargingFireball", false);
    }
}
