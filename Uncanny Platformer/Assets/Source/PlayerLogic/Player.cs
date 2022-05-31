using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float boxCastLength = 0.1f;

        [SerializeField] private LayerMask groundLayer;
        private BoxCollider2D boxCollider;
        
        public IRespawnable Respawn { get; private set; }
        public Rigidbody2D Body { get; private set; }
        public IControls Controls { get; private set; }
        public IMovement Movement { get; private set; }
        public IJump Jump { get; private set; }
        public IRangedCombat RangedCombat { get; private set; }
        public IHealth Health { get; private set; }

        public bool IsGrounded()
        {
            var bounds = boxCollider.bounds;
            
            var center = bounds.min;
            center.x += bounds.extents.x;
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

        private void Awake()
        {
            SetUpComponents();
            Health.Death += OnDeath;
            Respawn.Respawn += () => SetActive(true);
        }

        private void SetUpComponents()
        {
            Body = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            Controls = GetComponent<IControls>();
            Movement = GetComponent<IMovement>();
            Jump = GetComponent<IJump>();
            RangedCombat = GetComponent<IRangedCombat>();
            Health = GetComponent<IHealth>();
            Respawn = GetComponent<IRespawnable>();
        }

        private void SetActive(bool mode)
        {
            Controls.Switch(mode);
            Movement.Switch(mode);
        }

        private void OnDeath()
        {
            transform.SetParent(null);
            SetActive(false);
        }
    }
}
