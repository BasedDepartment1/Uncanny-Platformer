using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;
    private bool movingRight;

    [SerializeField] private float maxSpeed;

    private Vector3 initialScale;

    private Rigidbody2D enemyBody;

    [SerializeField] private float standDuration;

    private float standTimer;
    // Start is called before the first frame update
    void Awake()
    {
        initialScale = enemy.localScale;
        enemyBody = enemy.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (movingRight)
        // {
        //     
        // }
        if (movingRight)
        {
            if (enemy.position.x < rightEdge.position.x)
            {
                
                // enemyBody.velocity = new Vector2(maxSpeed, enemyBody.velocity.y);
                // enemy.position = new Vector3()
                MoveDirection(1);
            }
            else
            {
                Flip();
                // gameObject.SetActive(false);
            }
        }
        else
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveDirection(-1);
                // enemyBody.velocity = new Vector2(-maxSpeed, enemyBody.velocity.y);
            }
            else
            {
                Flip();
                // gameObject.SetActive(false);
            }
        }
    }

    private void MoveDirection(float direction)
    {
        standTimer = 0;
        enemy.localScale = new Vector3(-Mathf.Abs(initialScale.x) * direction, 
            initialScale.y, initialScale.z);
        // enemy.position = new Vector3(position.x + Time.deltaTime * direction * maxSpeed,
        //     position.y);
        enemyBody.velocity = new Vector2(direction * maxSpeed, enemyBody.velocity.y);
    }

    private void Flip()
    {
        standTimer += Time.deltaTime;
        if (standTimer > standDuration)
        {
            movingRight = !movingRight;
        }
        // var currentScale = enemy.localScale;
        // enemy.localScale = new Vector3(-currentScale.x,
        //     currentScale.y, currentScale.z);
    }
}
