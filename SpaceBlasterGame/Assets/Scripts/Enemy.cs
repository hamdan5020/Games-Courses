using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 500;
    [SerializeField] int ScoreValue = 100;
    
    [Header("Projectile Settings")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float enemyProjectileSpeed = 10f;
    
    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    
    [Header("Sound Settings")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float explosionSFXVolume = 0.1f;
    [SerializeField] AudioClip shotSFX;
    [SerializeField] [Range(0, 1)] float shotSFXVolume = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
       GameObject enemyLaser = Instantiate(enemyProjectile,
                transform.position,
                Quaternion.identity) as GameObject;

        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyProjectileSpeed);
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position, shotSFXVolume);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        HandleHit(damageDealer);
    }

    private void HandleHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        FindObjectOfType<GameSession>().AddToScore(ScoreValue);
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVolume);
        Destroy(gameObject);
    }

    
}
