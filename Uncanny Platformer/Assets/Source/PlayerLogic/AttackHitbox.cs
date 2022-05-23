using UnityEngine;

namespace Source.PlayerLogic
{
    public class AttackHitbox : MonoBehaviour
    {
        [SerializeField] private float damage;

        private bool isActive;

        public void SetHitboxStatus(bool status)
        {
            gameObject.SetActive(status);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ReduceHealthPoints(damage);
            }
        }
    }
}
