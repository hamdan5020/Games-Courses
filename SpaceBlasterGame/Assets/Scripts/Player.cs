using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config params
    [Header("Player")]
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float playerPadding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] float playerShootingPenalty = 25f;
    
    [Header("Projectile")]
    [SerializeField] GameObject playerLaser;
    [SerializeField] float playerLaserSpeed = 10f;
    [SerializeField] float playerLaserDelay = 1f;
    
    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    
    [Header("Sound Settings")]
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] [Range(0, 1)] float playerDeathSFXVolume = 0.1f;
    [SerializeField] AudioClip playerShotSFX;
    [SerializeField] [Range(0, 1)] float playerShotSFXVolume = 0.1f;


    float movementSpeedTemp;
    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounds();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            movementSpeedTemp = movementSpeed;
            movementSpeed = movementSpeed * (100 - playerShootingPenalty) / 100;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            movementSpeed = movementSpeedTemp;
        }

        IEnumerator FireContinuously()
        {
            while (true) 
            { 
            GameObject laser = Instantiate(playerLaser,
                transform.position,
                Quaternion.identity) as GameObject;

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerLaserSpeed);
            AudioSource.PlayClipAtPoint(playerShotSFX, 
                Camera.main.transform.position, 
                playerShotSFXVolume);



                yield return new WaitForSeconds(playerLaserDelay);
            }

        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBounds()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + playerPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - playerPadding;
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
        FindObjectOfType<Level>().LoadGameOver();
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, playerDeathSFXVolume);
        Destroy(gameObject);
        
    }
} 
 