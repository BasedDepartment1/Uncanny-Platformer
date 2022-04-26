using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] internal Controls controls;
    [SerializeField] internal PlayerMovement movement;
    [SerializeField] internal RangedCombat rangedCombat;
    [SerializeField] internal Health health;
    [SerializeField] internal AnimationController animationManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float deathTime = 1f;
    

    [SerializeField] private LayerMask groundLayer;
    
    internal Rigidbody2D body;
    private BoxCollider2D boxCollider;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.ReduceHealthPoints(50);
        }
        
        if (health.isDead)
        {
            controls.enabled = false;
            Invoke(nameof(Respawn), deathTime);
        }
    }

    private void Respawn()
    {
        health.Revive();
        transform.position = spawnPoint.position;
        controls.enabled = true;
    }
    
    internal bool IsGrounded()
    {
        var raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return raycastHit.collider != null;
    }
}
