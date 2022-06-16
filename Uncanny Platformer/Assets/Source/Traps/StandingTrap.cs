using UnityEngine;

namespace Source.Traps
{
    public class StandingTrap : MonoBehaviour
    {
        private MovingTrap platform;

        private void Start()
        {
            platform = GetComponent<MovingTrap>();
            platform.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            platform.enabled = true;
        }
    }
}
