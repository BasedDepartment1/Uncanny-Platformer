using Source.Interfaces;
using UnityEngine;

namespace Source.Traps
{
    public class Deathbox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IDamageable>()?.Kill();
        }
    }
}
