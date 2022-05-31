using Source.Interfaces;
using UnityEngine;

namespace Source.Traps
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IDamageable>()?.Damage(damage);
        }
    }
}
