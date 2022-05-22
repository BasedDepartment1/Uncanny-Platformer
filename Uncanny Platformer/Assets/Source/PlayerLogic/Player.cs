using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] internal Controls controls;
        [SerializeField] internal PlayerMovement movement;
        [SerializeField] internal Jump jump;
        [SerializeField] internal RangedCombat rangedCombat;
        [SerializeField] internal Health health;
        [SerializeField] internal AnimationController animationManager;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float deathTime = 1f;
        [SerializeField] private float boxCastLength = 0.1f;

        [SerializeField] private LayerMask groundLayer;
    
        internal Rigidbody2D Body;
        private BoxCollider2D boxCollider;
    
        void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            health.Death += Deactivate;
        }

        internal bool IsGrounded()
        {
            var center = boxCollider.bounds.min;
            center.x += boxCollider.bounds.extents.x;
            var size = new Vector2(boxCollider.size.x, 2 * boxCastLength);
            
            var raycastHit = Physics2D.BoxCast(
                center,
                size,
                0f,
                Vector2.up,
                boxCastLength,
                groundLayer);

            return raycastHit.collider != null;
        }

        private void Deactivate()
        {
            controls.enabled = false;
            movement.enabled = false;
        }

        public void Damage(float damage)
        {
            health.ReduceHealthPoints(damage);
        }

        public void Kill()
        {
            health.ReduceHealthPoints(health.CurrentHealth * 10);
        }
    }
}
