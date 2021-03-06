using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Jump : MonoBehaviour, IJump, ITossable
    {
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private int maxJumpCount = 3;
        
        private int jumpCount;

        private IPlayer Player { get; set; }

        public event Action<float> PerformJump;

        public void Toss(float force)
        {
            PerformJump?.Invoke(force);
        }

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
            if (jumpCount >= maxJumpCount) return;
            PerformJump(jumpForce);
            
        }

        private void MakeJump(float force)
        {
            Player.Body.velocity = new Vector2(Player.Body.velocity.x, force);
            jumpCount++;
        }
    }
}