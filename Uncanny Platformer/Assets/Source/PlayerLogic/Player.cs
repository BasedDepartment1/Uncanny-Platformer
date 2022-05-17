using JetBrains.Annotations;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable, ITossable
    {
        [SerializeField] internal Controls controls;
        [SerializeField] internal PlayerMovement movement;
        [SerializeField] internal Jump jump;
        [SerializeField] internal RangedCombat rangedCombat;
        [SerializeField] internal Health health;
        [SerializeField] internal AnimationController animationManager;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float deathTime = 1f;
    

        [SerializeField] private LayerMask groundLayer;
    
        internal Rigidbody2D Body;
        private BoxCollider2D boxCollider;
    
        void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
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
                Body.velocity = new Vector2(0f, Body.velocity.y);
                Invoke(nameof(Respawn), deathTime);
            }
        }

        private void Respawn()
        {
            health.Revive();
            Body.position = spawnPoint.position;
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

        public void Toss(float force)
        {
            jump.ActivateJump(force);
        }
    }
}
