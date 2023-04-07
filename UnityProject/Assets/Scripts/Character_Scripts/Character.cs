using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base script for script controlling a character 
/// </summary>
public class Character : MonoBehaviour
{
    [Header("Character")]
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed;

    [Tooltip("Attack damage of the character")]
    public int attackDamage;

    [Tooltip("Origin of the character's attack")]
    public Transform attackOrigin;

    [Tooltip("Melee attack radius of the character")]
    public float attackRadius;

    [Tooltip("LayerMask of objects that can be attacked by the character")]
    public LayerMask damageLayer;

    [Tooltip("Character model")]
    public GameObject character;

    // Info
    [Header("Info (character)")]
    [HideInInspector] public Rigidbody rigidBody;
    [Tooltip("Control character animation")]
    [HideInInspector] public Animator animator;

    // Start is called before the first frame update
    public virtual void Start()
    {
        TryGetComponent(out rigidBody);

        if (character)
        {
            character.TryGetComponent(out animator);
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        if (attackOrigin && attackRadius > 0.0f)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
        }
    }
#endif
}
