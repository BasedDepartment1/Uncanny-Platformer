using UnityEngine;

namespace Source.PlayerLogic
{
    public class ThrowingWeapon : MonoBehaviour
    {
        [SerializeField] private float knifeDamage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") 
                || other.CompareTag("Platform")
                || other.CompareTag("Trap")
                || other.GetComponent<Collider2D>().enabled == false)
            {
                return;
            }
        
            other.GetComponent<IDamageable>()?.Damage(knifeDamage);
            Destroy(gameObject);
        }
    }
}
