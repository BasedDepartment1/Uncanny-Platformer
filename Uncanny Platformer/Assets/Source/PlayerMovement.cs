using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerScript")] [SerializeField]
    private Player player;

    [Header("Movement characteristics")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumpCount = 3;

    internal bool isJumping;
    internal bool isRunning;
    private bool isOrientationRight = true;
    private int jumpCount;

    private void FixedUpdate()
    {
        if (player.controls.IsLeftPressed)
        {
            isRunning = true;
            MoveToDirection(Directions.Left);
        }
        else if (player.controls.IsRightPressed)
        {
            isRunning = true;
            MoveToDirection(Directions.Right);
        }
        else
        {
            isRunning = false;
            MoveToDirection(Directions.None);
        }

        if (player.IsGrounded())
        {
            jumpCount = 0;
        }
        
        if (player.controls.isJumpPressed)
        {
            isJumping = true;
            player.controls.isJumpPressed = false;
            Jump();
        }
    }

    private void MoveToDirection(Directions direction)
    {
        player.body.velocity = new Vector2((int)direction * maxSpeed, 
            player.body.velocity.y);
        
        if ((int)direction > 0 && !isOrientationRight
            || (int)direction < 0 && isOrientationRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        var transform1 = transform;
        var scale = transform1.localScale;
        scale.x *= -1;
        transform1.localScale = scale;
        isOrientationRight = !isOrientationRight;
    }
    
    private void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            player.body.velocity = new Vector2(player.body.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
