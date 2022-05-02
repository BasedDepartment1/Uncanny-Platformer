using UnityEngine;

public class Deathbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        var health = other.GetComponent<Health>();

        health.ReduceHealthPoints(health.CurrentHealth * 10);
    }
}
