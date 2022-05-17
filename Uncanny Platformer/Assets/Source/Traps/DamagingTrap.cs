using Source.Interfaces;
using UnityEngine;

namespace Source.Traps
{
    public class DamagingTrap : MonoBehaviour
    {
        [SerializeField] private float trapDamage = 25f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IDamageable>()?.Damage(trapDamage);
        }
    }
}
