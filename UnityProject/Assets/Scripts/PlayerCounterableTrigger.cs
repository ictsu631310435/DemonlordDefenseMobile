using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Make the Player be able to counterattack when enters the trigger
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class PlayerCounterableTrigger : MonoBehaviour
{
    private PlayerController _playerControl;

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerControl = other.GetComponent<PlayerController>();
            SetPlayerCanCounter(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerControl = other.GetComponent<PlayerController>();
            SetPlayerCanCounter(false);
        }
    }

    void OnDisable()
    {
        SetPlayerCanCounter(false);
    }

    void OnDestroy()
    {
        SetPlayerCanCounter(false);
    }

    void SetPlayerCanCounter (bool value)
    {
        if (!_playerControl || _playerControl.canCounter == value)
        {
            return;
        }
        _playerControl.canCounter = value;

        if (value)
        {
            _playerControl.ShowCounterableIndicator();
        }
    }
}
