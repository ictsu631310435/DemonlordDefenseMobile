using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling a Knight Enemy
/// </summary>
public class KnightEnemy : Enemy
{
    [Header("Knight Enemy")]
    public GameObject playerCounterableTrigger;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        playerCounterableTrigger.transform.position = transform.position;
        playerCounterableTrigger.transform.rotation = transform.rotation;
    }

    // Method for attacking (Melee)
    public override void Attack()
    {
        // Array of all Colliders in damageLayer caught in Range.
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRadius, damageLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out HealthController healthController))
            {
                healthController.ChangeHealth(-attackDamage);
            }
        }
    }
}
