using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] int projectileDamage = 100;
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<Health>();
        var attacker = collision.GetComponent<Attacker>();

        if (health && attacker)
        {
            health.DealDamage(projectileDamage);
            Destroy(gameObject);
        }
        
    }

    
}
