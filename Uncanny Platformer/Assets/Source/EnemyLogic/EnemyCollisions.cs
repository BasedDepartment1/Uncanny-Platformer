using Source.PlayerLogic;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyCollisions : MonoBehaviour
    {
        [SerializeField] private float damage;

        [SerializeField] private Player player;

        private void Start()
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                player.GetComponent<Collider2D>(), true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled) return;
            
            other.GetComponent<IDamageable>()?.Damage(damage);
        }
    }
}
