using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for containing information of an Enemy Wave
/// </summary>
[CreateAssetMenu(fileName = "Wave_New", menuName = "ScriptableObject/Enemy Wave")]
public class Wave : ScriptableObject
{
    public GameObject[] enemies;
    public float secondsBeforeWave;
    public int numberToSpawn;
}
