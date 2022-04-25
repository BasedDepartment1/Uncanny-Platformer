using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private int jumpCount;
    [SerializeField] private int MaxJumpCount = 3;
    
    private bool isOrientationRight = true;
    
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (IsGrounded())
        {
            animator.SetBool("isFalling", false);
        }
        if (Mathf.Sign(body.velocity.y) > -1f)
        {
            return;
        }
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", true);
    }

    public void MoveToDirection(Directions direction)
    {
        body.velocity = new Vector2((int)direction * MaxSpeed, body.velocity.y);
        animator.SetBool("isRunning", direction != Directions.None);
        if ((int)direction > 0 && !isOrientationRight
            || (int)(direction) < 0 && isOrientationRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isOrientationRight = !isOrientationRight;
    }
    
    public void Jump()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
        }

        if (jumpCount < MaxJumpCount)
        {
            animator.SetBool("isJumping", true);
            body.velocity = new Vector2(body.velocity.x, MaxSpeed);
            jumpCount++;
        }
    }

    private bool IsGrounded()
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

