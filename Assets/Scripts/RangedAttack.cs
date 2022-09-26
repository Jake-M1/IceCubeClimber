using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject playerProjectilePrefab;
    public float rangedAttackRate = 2f;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Shoot();
                nextAttackTime = Time.time + 1f/rangedAttackRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(playerProjectilePrefab, firePoint.position, firePoint.rotation);
    }
}
