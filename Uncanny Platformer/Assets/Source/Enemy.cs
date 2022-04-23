using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    // private bool movingRight;
    // // private float leftEdge;
    // // private float rightEdge;
    // [SerializeField] private Transform leftEdge;
    // [SerializeField] private Transform rightEdge;
    // private Rigidbody2D enemyBody;
    // [SerializeField] private float maxSpeed = 10f;
    // [SerializeField] private float maxDistance;
    
    private void Awake()
    {
        // var position = transform.position;
        // enemyBody = GetComponent<Rigidbody2D>();
        // leftEdge = position.x - maxDistance;
        // rightEdge = position.x + maxDistance;
    }
    
    private void Update()
    {
        // if (movingRight)
        // {
        //     if (transform.position.x < rightEdge.position.x)
        //     {
        //         enemyBody.velocity = new Vector2(maxSpeed, enemyBody.velocity.y);
        //     }
        //     else
        //     {
        //         // Flip();
        //         gameObject.SetActive(false);
        //     }
        // }
        // else
        // {
        //     if (transform.position.x > leftEdge.position.x)
        //     {
        //         enemyBody.velocity = new Vector2(-maxSpeed, enemyBody.velocity.y);
        //     }
        //     else
        //     {
        //         // Flip();
        //         gameObject.SetActive(false);
        //     }
        // }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            // other.GetComponent<Rigidbody2D>().velocity =
            //     new Vector2(other.GetComponent<Rigidbody2D>().velocity.x,
            //         10f);
        }
    }
    
    // private void Flip()
    // {
    //     var scale = transform.localScale;
    //     scale.x *= -1;
    //     transform.localScale = scale;
    //     movingRight = !movingRight;
    // }
    // [SerializeField] private float pushForceHorizontal = 1000f;
    //
    // [SerializeField] private float pushForceVertical = 0f;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         var player = other.gameObject;
    //         // player.GetComponent<Health>().TakeDamage(20);
    //         var playerBody = player.GetComponent<Rigidbody2D>();
    //         // playerBody.velocity =
    //             // new Vector2(-Mathf.Sign(playerBody.velocity.x) * pushForceHorizontal,
    //             // pushForceVertical);
    //             // new Vector2(-1010f, 10f);
    //         // playerBody.AddForce(new Vector2(-Mathf.Sign(playerBody.velocity.x) * pushForceHorizontal,
    //             // pushForceVertical));
    //         // player.GetComponent<PlayerMovement>().KnockBack();
    //     }
    // }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                other.collider, true);
        }
    }
    
}
