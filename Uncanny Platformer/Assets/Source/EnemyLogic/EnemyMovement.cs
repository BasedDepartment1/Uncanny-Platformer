using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] internal Enemy enemy;
        [SerializeField] private float speed = 5;

        internal bool isMoving;
    
        internal void MoveToDirection(Directions direction)
        {
            var initialScale = enemy.transform.localScale;
            enemy.transform.localScale = new Vector3(-Mathf.Abs(initialScale.x) * (int)direction, 
                initialScale.y, initialScale.z);
            enemy.body.velocity = new Vector2((int)direction * speed, enemy.body.velocity.y);
        }
    }
}
