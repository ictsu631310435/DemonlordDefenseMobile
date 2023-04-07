using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    private PlayerInputReceiver _input;

    public GameObject[] fireball;

    public Transform StatusIndicator;
    public GameObject alertParticle;

    public GameObject currentIndicator;

    public bool canRotate;
    public bool canCounter;

    public int maxMana;
    public float regenCooldown;
    public float regenTickDuration;
    public int regenAmount;

    public int currentMana;
    public float currentRegenCooldown;
    public float currentRegenTick;
    public bool enableRegen;

    public ValueBar manaBar;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        _input = GetComponent<PlayerInputReceiver>();

        canRotate = true;

        currentMana = maxMana;
        manaBar.InitValue(currentMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMana == maxMana || enableRegen == false)
        {
            return;
        }

        if (currentRegenCooldown > 0f)
        {
            currentRegenCooldown -= Time.deltaTime;
            return;
        }

        if (currentRegenTick > 0f)
        {
            currentRegenTick -= Time.deltaTime;
        }
        else
        {
            ChangeMana(regenAmount);
            currentRegenTick = regenTickDuration;
        }
    }

    public void SwordHit()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackOrigin.position, attackRadius, damageLayer);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent(out HealthController health))
            {
                health.ChangeHealth(-attackDamage);
            }
            else
            if (collider.TryGetComponent(out Projectile projectile))
            {
                if (GameController.Instance)
                {
                    GameController.Instance.UpdateScore(projectile.scoreValue);
                }
                Destroy(projectile.gameObject);

                Instantiate(fireball[0], attackOrigin.position, transform.rotation);
            }

            ChangeMana(+1);
        }
    }

    public void ShootFireball()
    {
        Instantiate(fireball[_input.chargeLevel], attackOrigin.position, transform.rotation);
        _input.chargeLevel = 0;
    }

    public void ShowCounterableIndicator()
    {
        currentIndicator = Instantiate(alertParticle, StatusIndicator);
    }

    public void ChangeMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        manaBar.SetValue(currentMana);

        if (amount < 0)
        {
            currentRegenCooldown = regenCooldown;
            currentRegenTick = regenTickDuration;
        }

        if (currentMana == maxMana)
        {
            enableRegen = false;
        }
    }
}
