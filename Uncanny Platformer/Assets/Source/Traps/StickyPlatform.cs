using Source.Interfaces;
using UnityEngine;

namespace Source.Traps
{
    public class StickyPlatform : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var isDead = other.GetComponent<IHealth>()?.IsDead;
            
            if (isDead != null && (bool)isDead
             || !other.gameObject.CompareTag("Player")) return;
        
            other.gameObject.transform.SetParent(transform);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
        
            other.gameObject.transform.SetParent(null);
        }
    }
}
