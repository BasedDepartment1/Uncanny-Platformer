using JetBrains.Annotations;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] internal Controls controls;
        [SerializeField] internal PlayerMovement movement;
        [SerializeField] internal RangedCombat rangedCombat;
        [SerializeField] internal Health health;
        [SerializeField] internal AnimationController animationManager;
        [SerializeField] [CanBeNull] private Transform spawnPoint;
        [SerializeField] private float deathTime = 1f;
    

        [SerializeField] private LayerMask groundLayer;
    
        internal Rigidbody2D body;
        private BoxCollider2D boxCollider;
    
        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                health.ReduceHealthPoints(50);
            }
        
            if (health.IsDead)
            {
                controls.enabled = false;
                body.velocity = new Vector2(0f, body.velocity.y);
                Invoke(nameof(Respawn), deathTime);
            }
        }

        private void Respawn()
        {
            health.Revive();
            body.position = spawnPoint.position;
            controls.enabled = true;
        }
    
        internal bool IsGrounded()
        {
            var raycastHit = Physics2D.BoxCast(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0f,
                Vector2.down,
                0.1f,
                groundLayer
            );

            return raycastHit.collider != null;
        }

        public void Damage(float damage)
        {
            health.ReduceHealthPoints(damage);
        }
    }
}
