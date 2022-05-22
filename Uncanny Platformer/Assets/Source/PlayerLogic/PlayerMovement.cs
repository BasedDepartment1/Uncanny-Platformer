using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [Header("PlayerScript")] [SerializeField]
        private Player player;

        [Header("Movement characteristics")]
        [SerializeField] private float maxSpeed = 10f;

        private bool isOrientationRight = true;
        
        public event Action<Directions> Move;

        public event Action Idle;

        private void Start()
        {
            Move += MoveToDirection;
            Idle += () => MoveToDirection(Directions.None);
        }

        private void FixedUpdate()
        {
            if (!enabled)
            {
                player.Body.velocity = new Vector2(0f, player.Body.velocity.y);
            }
            if (player.controls.IsLeftPressed)
            {
                Move(Directions.Left);
            }
            else if (player.controls.IsRightPressed)
            {
                Move(Directions.Right);
            }
            else
            {
                Idle();
            }
        }

        private void MoveToDirection(Directions direction)
        {
            player.Body.velocity = new Vector2((int)direction * maxSpeed, 
                player.Body.velocity.y);
        
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
    }
}
