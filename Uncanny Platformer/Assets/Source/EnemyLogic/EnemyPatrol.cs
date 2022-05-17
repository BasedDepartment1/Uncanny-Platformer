using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyPatrol : MonoBehaviour
    {
        [Header("Patrol Objects")]
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;
        [SerializeField] private Enemy enemy;
    
        [Header("Patrol Timings")]
        [SerializeField] private float standDuration = 1;
    
    
        private bool movingRight;
        private float standTimer;

        void FixedUpdate()
        {
            if (!enabled) return;
        
            if (movingRight)
            {
                if (enemy.transform.position.x < rightEdge.position.x)
                {
                    StartMoving(Directions.Right);
                }
                else
                {
                    TurnAround();
                }
            }
            else
            {
                if (enemy.transform.position.x >= leftEdge.position.x)
                {
                    StartMoving(Directions.Left);
                }
                else
                {
                    TurnAround();
                }
            }
        }

        private void TurnAround()
        {
            enemy.movement.isMoving = false;
            standTimer += Time.deltaTime;
            if (standTimer > standDuration)
            {
                movingRight = !movingRight;
            }
        }

        private void StartMoving(Directions direction)
        {
            enemy.movement.isMoving = true;
            standTimer = 0;
            enemy.movement.MoveToDirection(direction);
        }
    }
}
