
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportPosition;
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.GetComponent<Rigidbody2D>().position = 
                (new Vector3(teleportPosition.position.x, teleportPosition.position.y, transform.position.z));
        }
    }
}

