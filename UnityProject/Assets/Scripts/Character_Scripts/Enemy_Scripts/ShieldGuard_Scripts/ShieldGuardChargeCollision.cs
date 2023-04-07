using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardChargeCollision : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;

    public float playerStunDuration;

    private ShieldGuardEnemy _shieldGuard;

    // Start is called before the first frame update
    void Start()
    {
        _shieldGuard = GetComponentInParent<ShieldGuardEnemy>();
    }

    // OnTriggerEnter is called when the Collider collision enter the trigger
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") ||
            1 << collision.gameObject.layer == obstacleLayer.value || 1 << collision.gameObject.layer == enemyLayer.value)
        {
            if (collision.CompareTag("Player"))
            {
                _shieldGuard.whatsHit = ShieldGuardEnemy.WhatsHit.Player;

                PlayerInputReceiver playerInput = collision.GetComponent<PlayerInputReceiver>();
                playerInput.GetStun(playerStunDuration);

                HealthController health = collision.GetComponent<HealthController>();
                health.ChangeHealth(-_shieldGuard.attackDamage);
            }
            else
            {
                _shieldGuard.whatsHit = ShieldGuardEnemy.WhatsHit.Obstacle;
            }

            _shieldGuard.hitSomething = true;
            gameObject.SetActive(false);
        }
    }
}
