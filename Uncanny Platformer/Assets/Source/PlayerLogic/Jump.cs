using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Jump : MonoBehaviour, IJump, ITossable
    {
        [Header("PlayerScript")] [SerializeField]
        private Player player;
        
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private int maxJumpCount = 3;
        
        private int jumpCount;

        public event Action<float> PerformJump;

        private void Start()
        {
            PerformJump += ActivateJump;
        }

        private void FixedUpdate()
        {
            if (player.IsGrounded())
            {
                jumpCount = 0;
            }

            if (!player.controls.IsJumpPressed) return;
            player.controls.IsJumpPressed = false;
            
            PerformJump(jumpForce);
            
        }
        
        public void ActivateJump(float force)
        {
            if (jumpCount < maxJumpCount)
            {
                player.Body.velocity = new Vector2(player.Body.velocity.x, force);
                jumpCount++;
            }
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