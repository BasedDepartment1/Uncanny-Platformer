using Source.Interfaces;
using Source.PlayerLogic;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyCollisions : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void Start()
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                FindObjectOfType<Player>().GetComponent<Collider2D>(), true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled) return;
            
            other.GetComponent<IDamageable>()?.Damage(damage);
        }
    }
}
