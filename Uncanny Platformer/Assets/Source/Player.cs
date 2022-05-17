using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] internal Controls controls;
    [SerializeField] internal PlayerMovement movement;
    [SerializeField] internal RangedCombat rangedCombat;
    [SerializeField] internal Health health;
    [SerializeField] internal AnimationController animationManager;
    [SerializeField] [CanBeNull] private Transform spawnPoint;
    [SerializeField] private float deathTime = 10f;
    

    [SerializeField] private LayerMask groundLayer;
    
    internal Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private bool isDeadSoundNeed = true;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.ReduceHealthPoints(50);
        }
        
        if (health.isDead)
        {
            //StartCoroutine(MakeDeathSound());        rewrite MakeDeathSound with Coroutine
            MakeDeathSound();
            controls.enabled = false;
            body.velocity = new Vector2(0f, body.velocity.y);
            Invoke(nameof(Respawn), deathTime);
        }
    }

    private void Respawn()
    {
        health.Revive();
        isDeadSoundNeed = true;
        body.position = spawnPoint.position;
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

    private void MakeDeathSound()
    {
        if (isDeadSoundNeed)
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        isDeadSoundNeed = false;
    }
}
