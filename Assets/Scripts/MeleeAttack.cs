using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 2f;
    public int meleeDamage = 15;
    public float meleeAttackRate = 2f;
    private float nextAttackTime = 0f;
    public int kbStrength = 800;
    private bool rightAttack = false;

    public Animator player_animator;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f/meleeAttackRate;
            }
        }
    }

    void Attack()
    {
        rightAttack = false;
        if (transform.rotation.y * 180 >= -1)
        {
            rightAttack = true;
        }

        player_animator.SetTrigger("MeleeAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.isTrigger == false)
            {
                if (rightAttack)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(meleeDamage, kbStrength);
                }
                else if (!rightAttack)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(meleeDamage, kbStrength * -1);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
