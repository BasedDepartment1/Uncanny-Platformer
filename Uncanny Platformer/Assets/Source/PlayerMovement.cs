using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private int MaxJumpCount = 3;
    
    private bool isOrientationRight = true;
    private Rigidbody2D body;
    private int jumpCount;
    private Animator animator;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (body.velocity.y >= 0)
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
        animator.SetBool("isJumping", true);
        
        if (MaxJumpCount > jumpCount)
        {
            body.velocity = new Vector2(body.velocity.x, MaxSpeed);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isFalling", false);
            jumpCount = 0;
        }
    }
}