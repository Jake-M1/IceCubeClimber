using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    private Rigidbody2D e_Rigidbody2D;

    void Start()
    {
        currentHealth = maxHealth;
        e_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, int kbStrength)
    {
        currentHealth -= damage;

        TakeKnockback(kbStrength);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void TakeKnockback(int strength)
    {
        e_Rigidbody2D.AddForce(new Vector2(strength, Mathf.Abs(strength)));
    }
}
