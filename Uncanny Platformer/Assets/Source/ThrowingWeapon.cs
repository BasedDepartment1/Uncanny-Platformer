using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    [SerializeField] private float knifeDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        
        other.GetComponent<Health>()?.ReduceHealthPoints(knifeDamage);
        Destroy(gameObject);
    }
}
