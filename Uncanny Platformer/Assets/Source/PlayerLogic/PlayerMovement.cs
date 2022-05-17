using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("PlayerScript")] [SerializeField]
        private Player player;

        [Header("Movement characteristics")]
        [SerializeField] private float maxSpeed = 10f;
        
        internal bool IsRunning;
        private bool isOrientationRight = true;

        private void FixedUpdate()
        {
            if (player.controls.IsLeftPressed)
            {
                IsRunning = true;
                MoveToDirection(Directions.Left);
            }
            else if (player.controls.IsRightPressed)
            {
                IsRunning = true;
                MoveToDirection(Directions.Right);
            }
            else
            {
                IsRunning = false;
                MoveToDirection(Directions.None);
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
