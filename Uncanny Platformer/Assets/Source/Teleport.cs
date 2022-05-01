
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportPosition;
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("А БЛЯТЬ");
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.transform.Translate(teleportPosition.position);
        }
    }
}

