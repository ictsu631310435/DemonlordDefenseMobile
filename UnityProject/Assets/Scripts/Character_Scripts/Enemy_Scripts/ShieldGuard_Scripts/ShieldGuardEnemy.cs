using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuardEnemy : Enemy
{
    [Header("Shield Guard Enemy")]
    public float chargeDuration;

    public GameObject chargeHitBox;

    public float pushBackDuration;
    public float pushBackSpeed;

    [HideInInspector] public bool hitSomething;
    [HideInInspector] public bool hitPlayer;
    [HideInInspector] public bool hitObstacle;

    public enum WhatsHit
    {
        Nothing, Player, Obstacle
    }
    public WhatsHit whatsHit;

    public GameObject stunIndicator;

    public override void Start()
    {
        base.Start();

        LookAt(player);
    }

    public void GetStun()
    {
        GetComponent<Animator>().SetBool("isStun", true);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
