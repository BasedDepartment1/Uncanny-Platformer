using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Jump : MonoBehaviour, IJump, ITossable
    {
        // [Header("PlayerScript")] [SerializeField]
        // private Player player;
        
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private int maxJumpCount = 3;
        
        private int jumpCount;

        private IPlayer Player { get; set; }

        public event Action<float> PerformJump;

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            PerformJump += MakeJump;
        }

        private void FixedUpdate()
        {
            if (Player.IsGrounded())
            {
                jumpCount = 0;
            }

            if (!Player.Controls.IsJumpPressed) return;
            Player.Controls.IsJumpPressed = false;
            
            PerformJump(jumpForce);
            
        }
        
        private void MakeJump(float force)
        {
            if (jumpCount >= maxJumpCount) return;
            
            Player.Body.velocity = new Vector2(Player.Body.velocity.x, force);
            jumpCount++;
        }
        
        public void Toss(float force)
        {
            if (PerformJump != null)
            {
                PerformJump(force);
            }
        }
    }
}