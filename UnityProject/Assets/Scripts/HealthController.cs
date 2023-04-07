using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Script for controlling health of the character
/// </summary>
public class HealthController : MonoBehaviour
{
    #region Data Members
    [Tooltip("Maximum health of the character")]
    public int maxHealth;

    [Tooltip("Current health of the character")]
    [field: SerializeField] public int CurrentHealth { get; private set; } // Other can readonly

    // Events
    public UnityEvent<int> OnHealthInitialized;
    public UnityEvent<int> OnHealthChanged;
    public UnityEvent OnHealthDepleted;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        // Set Initial Health
        CurrentHealth = maxHealth;

        // Invoke HealthSet event
        OnHealthInitialized?.Invoke(CurrentHealth);
    }
    #endregion

    #region Methods
    // Method for change CurrentHealth's value
    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth); // Health cannot go below 0

        // Invoke HealthChanged event
        OnHealthChanged?.Invoke(CurrentHealth);

        // Check if Health reach 0
        if (CurrentHealth == 0)
        {
            // Invoke HealthDepleted event
            OnHealthDepleted?.Invoke();
        }
    }
    #endregion
}
