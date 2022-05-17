using Source.Interfaces;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] internal Health health;
        [SerializeField] internal EnemyPatrol patrolBehaviour;
        [SerializeField] internal EnemyAnimations animations;
        [SerializeField] internal EnemyMovement movement;
        [SerializeField] internal EnemyCollisions collisions;
    
        internal Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (health.IsDead)
            {
                collisions.enabled = false;
                patrolBehaviour.enabled = false;
            }
        }

        public void Damage(float damage)
        {
            health.ReduceHealthPoints(damage);
        }
    }
}
