using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] internal Health health;
    [SerializeField] internal EnemyPatrol patrolBehaviour;
    [SerializeField] internal EnemyAnimations animations;
    [SerializeField] internal EnemyMovement movement;
    [SerializeField] internal EnemyCollisions collisions;
    
    internal Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health.isDead)
        {
            collisions.enabled = false;
            patrolBehaviour.enabled = false;
        }
    }
}
