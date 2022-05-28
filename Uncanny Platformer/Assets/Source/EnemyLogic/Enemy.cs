using Source.PlayerLogic;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private EnemyCollisions collisions;
        [SerializeField] private bool isLevel3;
        
        public IHealth Health { get; private set; }
        public IMovement PatrolBehaviour { get; private set; }
        public Rigidbody2D Body { get; private set; }

        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            Health = GetComponent<IHealth>();
            PatrolBehaviour = GetComponent<IMovement>();
            Health.Death += Deactivate;
        }

        private void Deactivate()
        {
            collisions.enabled = false;
            PatrolBehaviour.Switch(false);
            Body.gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            if (isLevel3)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
