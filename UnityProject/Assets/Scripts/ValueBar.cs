using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script that can be use for controlling various bars, e.g., health, mana
/// </summary>
public class ValueBar : MonoBehaviour
{
    private Slider _slider;

    // Awakr is called when the script instance is being loaded
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    #region Methods
    // Method for initialize slider value
    public void InitValue(int value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }

    // Method for set slider value
    public void SetValue(int value)
    {
        _slider.value = value;
    }
    #endregion
}
