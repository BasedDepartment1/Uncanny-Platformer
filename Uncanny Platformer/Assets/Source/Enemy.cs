using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().ReduceHealthPoints(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                other.collider, true);
        }
    }
    
}
