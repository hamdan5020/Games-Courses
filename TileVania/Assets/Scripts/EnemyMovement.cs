using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    [SerializeField] GameObject front;
    int facing = 1;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private bool HasReachedLedge()
    {
        if (front.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return false;
        }else
        {
            return true;
        }
    }


    private void Move()
    {

        
        if (HasReachedLedge())
        {
            facing = facing * -1;
        }

        myRigidBody.velocity = new Vector2(facing * moveSpeed, 0);
        transform.localScale = new Vector2(facing, 1);
    }


}
