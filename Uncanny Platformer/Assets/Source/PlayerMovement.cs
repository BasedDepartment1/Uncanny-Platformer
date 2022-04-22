using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed = 10f;
    private bool isOrientationRight = true;
    private Rigidbody2D body;
    public int jumpCount;
    public int MaxJumpCount = 3;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        var moveDistance = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveDistance * MaxSpeed,
            body.velocity.y);
        if (moveDistance > 0 && !isOrientationRight
            || moveDistance < 0 && isOrientationRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.W) && MaxJumpCount > jumpCount)
        {
            Jump();
            jumpCount++;
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isOrientationRight = !isOrientationRight;
    }

    private void Jump() => body.velocity = new Vector2(body.velocity.x, MaxSpeed);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}

