using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject feet;
    [SerializeField] Vector2 deathFling = new Vector2(25f, 25f);
    float originalJumpSpeed;
    bool isAlive = true;


    Animator playerAnimator;
    float originalGravityScale;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        originalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
        originalJumpSpeed = jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isAlive)
        {
            return;
        }
        Die();
        Run();
        jump();
        Climb();
        
    }

    private void Run()
    {
        float hIn = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2(hIn * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (hIn < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
            playerAnimator.SetBool("running", true);
        }
        else if (hIn > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
            playerAnimator.SetBool("running", true);
        }
        else
        {
            playerAnimator.SetBool("running", false);
        }

    }


    private void jump()
    {

        if (!feet.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            
            return;
        }
      

        if (Input.GetButtonDown("Jump"))
        {
            
            Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
            GetComponent<Rigidbody2D>().velocity += jumpVelToAdd;
        }
    }

    private void Climb()
    {
        if (!GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            playerAnimator.SetBool("climbing", false);
            GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
            playerAnimator.speed = 1;
            jumpSpeed = originalJumpSpeed;
            return;
            
        }

        if (!isAlive)
        {
            return;
        }

        jumpSpeed = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        float vIn = Input.GetAxis("Vertical");
        if (vIn < 0 || vIn > 0)
        {
            playerAnimator.speed = 1;
        }else
        {
            playerAnimator.speed = 0;
        }
        playerAnimator.SetBool("climbing", true);
        transform.Translate(new Vector2(0, vIn) * climbSpeed * Time.deltaTime);
    }

    private void Die()
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("die");

            GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
            GetComponent<Rigidbody2D>().velocity = deathFling;
            
        }
    }

    


}
