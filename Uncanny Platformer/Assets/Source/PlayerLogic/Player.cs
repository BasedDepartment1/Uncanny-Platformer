using UnityEngine;

namespace Source.PlayerLogic
{
    public class Player : MonoBehaviour, IPlayer
    {
        // [SerializeField] public Controls controls;
        // [SerializeField] internal PlayerMovement movement;
        // [SerializeField] internal Jump jump;
        // [SerializeField] internal RangedCombat rangedCombat;
        // [SerializeField] internal Health health;
        // public 
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float deathTime = 1f;
        [SerializeField] private float boxCastLength = 0.1f;

        [SerializeField] private LayerMask groundLayer;
        private BoxCollider2D boxCollider;

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
            // health.Death += Deactivate;
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
        }

        // private void Deactivate()
        // {
        //     Controls.enabled = false;
        //     Movement.enabled = false;
        // }
    }
}
