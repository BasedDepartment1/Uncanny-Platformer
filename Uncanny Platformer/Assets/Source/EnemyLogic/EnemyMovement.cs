using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] internal Enemy enemy;
        [SerializeField] private float speed = 5;

        internal bool isMoving;
    
        internal void MoveToDirection(int direction)
        {
            var initialScale = enemy.transform.localScale;
            enemy.transform.localScale = new Vector3(-Mathf.Abs(initialScale.x) * direction, 
                initialScale.y, initialScale.z);
            enemy.Body.velocity = new Vector2(direction * speed, enemy.Body.velocity.y);
        }

        internal void MoveToDirection(Directions direction)
        {
            MoveToDirection((int)direction);
        }
    }
}
