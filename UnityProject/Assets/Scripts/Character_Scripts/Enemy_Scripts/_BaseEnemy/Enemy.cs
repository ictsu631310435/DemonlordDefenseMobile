using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// Base script for script controlling a enemy 
/// </summary>
public abstract class Enemy : Character
{
    #region Data Members
    [Header("Enemy")]
    public float rotationSpeed;

    public float lockOnRange;

    public float attackRange;
    public float secondsBtwnAttacks;

    public int scoreValue;

    // Info
    [Header("Info (enemy)")]
    [HideInInspector] public float speed;

    [HideInInspector] public Transform player;
    [HideInInspector] public float distanceFromPlayer;

    [HideInInspector] public Vector3 direction;

    [HideInInspector] public float nextAttackTime;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (player)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        }
    }

#if UNITY_EDITOR
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        // Draw attackRange
        if (attackRange > 0.0f)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
#endif
    #endregion

    #region Methods
    // Method for rotating Enemy to face Player
    public void LookAt(Transform target)
    {
        Vector3 heading = target.position - transform.position;
        direction = heading.normalized;

        // Caculate rotation that Character is going to
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        // Rotate Character rotation toward toRotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    // Method for moving Enemy at targetSpeed
    public void MoveAtSpeed(float targetSpeed)
    {
        if (speed != targetSpeed)
        {
            speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime);
        } else
        { 
            speed = targetSpeed;
        }

        if (speed == 0f)
        {
            direction = Vector3.zero;
        }

        rigidBody.velocity = speed * direction;
    }

    // Method for attacking
    public abstract void Attack();
    
    // Method for transitioning to Dead State
    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        GetComponent<Animator>().SetBool("isDead", true);
        if (character)
        {
            animator.SetTrigger("Die");
        }

        if (GameController.Instance)
        {
            GameController.Instance.UpdateScore(scoreValue);
        }
    }
    #endregion
}
