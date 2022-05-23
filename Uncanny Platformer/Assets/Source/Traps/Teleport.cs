
using UnityEngine;

namespace Source.Traps
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform teleportPosition;
  
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag("Player")) return;
            
            var position = teleportPosition.position;
            collider.GetComponent<Rigidbody2D>().position = 
                (new Vector3(position.x, position.y, transform.position.z));
        }
    }
}

