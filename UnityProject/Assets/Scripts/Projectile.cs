using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for controlling a projectile
/// </summary>
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    #region Data Members
    public float moveSpeed;

    public LayerMask obstacleLayer;
    public LayerMask damageLayer;

    public int attackDamage;

    public int scoreValue;

    public int enemyHitLimit;
    public int hitNum;
    #endregion

    #region Unity Callbacks
    // Update is called once per frame
    void Update()
    {
        // Move the projectile
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }

    // OnTriggerEnter is called when the Collider other enter the trigger
    void OnTriggerEnter(Collider collision)
    {
        if (1 << collision.gameObject.layer == obstacleLayer.value)
        {
            Destroy(gameObject);
        }
        else if (1 << collision.gameObject.layer == damageLayer.value)
        {
            if (collision.TryGetComponent(out HealthController health))
            {
                // Inflict attackDamage
                health.ChangeHealth(-attackDamage);
            }
            else if (collision.TryGetComponent(out Projectile projectile))
            {
                if (GameController.Instance)
                {
                    GameController.Instance.UpdateScore(projectile.scoreValue);
                }
                Destroy(collision.gameObject);
            }

            hitNum++;
            if (hitNum >= enemyHitLimit)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
