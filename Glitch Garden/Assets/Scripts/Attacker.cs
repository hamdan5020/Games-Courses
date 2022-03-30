using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Attacker : MonoBehaviour
{

    float crntSpeed = 1f;
    GameObject crntTrgt;
    Animator animator;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();

    }
    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();

        if (levelController)
        {
            levelController.Attackerkilled();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * crntSpeed * Time.deltaTime);
        animator = GetComponent<Animator>();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!crntTrgt)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        crntSpeed = speed;
    }

    public void Attack(GameObject target)
    {
        animator.SetBool("isAttacking", true);
        crntTrgt = target;
        if (!target)
        {
            return;
        }
    }

    public void StrikeCurrentTarget(int damage)
    {
        if (!crntTrgt)
        {


            return;
        }

        Health health = crntTrgt.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(damage);
        }
    }

    
}
