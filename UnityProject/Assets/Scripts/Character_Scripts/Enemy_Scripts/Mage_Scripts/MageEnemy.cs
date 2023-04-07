using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling a Mage Enemy
/// </summary>
public class MageEnemy : Enemy
{
    [Header("Mage Enemy")]
    public GameObject fireballPrefab;

    // Method for attacking (Ranged)
    public override void Attack()
    {
        Instantiate(fireballPrefab, attackOrigin.position, transform.rotation);
    }
}
