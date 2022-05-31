using Source;
using UnityEngine;

public class jabka : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        FindObjectOfType<AudioManager>().Play("Quack", out _);
    }
}
