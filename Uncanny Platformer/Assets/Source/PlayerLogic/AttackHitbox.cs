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
            other.GetComponent<IDamageable>()?.Damage(damage);
        }
    }
}
