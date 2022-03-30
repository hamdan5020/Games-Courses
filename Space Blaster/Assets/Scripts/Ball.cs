using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Config
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchX = 0f;
    [SerializeField] float launchY = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //cashed
    Rigidbody2D myRigidbody2D;

    //States
    bool hasStarted = false;
    Vector2 paddleToBallVector;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
        
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(launchX, launchY);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float x = UnityEngine.Random.Range(0, randomFactor);
        float y = UnityEngine.Random.Range(0, randomFactor);

        Vector2 velocityTweak = new Vector2(x, y);
        if (hasStarted)
        {
            //GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0, 3);
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
        
    }
}
