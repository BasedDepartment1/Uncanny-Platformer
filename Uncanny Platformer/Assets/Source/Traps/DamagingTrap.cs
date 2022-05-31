using Source.Interfaces;
using UnityEngine;

namespace Source.Traps
{
    public class DamagingTrap : MonoBehaviour
    {
        [SerializeField] private float trapDamage = 25f;
        [SerializeField] private float cooldown = 0.25f;

        private float cooldownTimer;
        
        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!(cooldownTimer > cooldown)) return;
            
            other.GetComponent<IDamageable>()?.Damage(trapDamage);
            cooldownTimer = 0;
        }
    }
}
