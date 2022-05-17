using UnityEngine;

namespace Source.PlayerLogic
{
    public class Jump : MonoBehaviour
    {
        [Header("PlayerScript")] [SerializeField]
        private Player player;
        
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private int maxJumpCount = 3;
        
        private int jumpCount;
        internal bool IsJumping;

        private void FixedUpdate()
        {
            if (player.IsGrounded())
            {
                jumpCount = 0;
            }
        
            if (player.controls.IsJumpPressed)
            {
                player.controls.IsJumpPressed = false;
                ActivateJump(jumpForce);
            }
        }
        
        internal void ActivateJump(float force)
        {
            IsJumping = true;
            if (jumpCount < maxJumpCount)
            {
                player.Body.velocity = new Vector2(player.Body.velocity.x, force);
                jumpCount++;
            }
        }
    }
}