using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class ThrowingWeapon : MonoBehaviour
    {
        [SerializeField] private float knifeDamage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                return;
            }
        
            other.GetComponent<IDamageable>()?.Damage(knifeDamage);
            Destroy(gameObject);
        }
    }
}
