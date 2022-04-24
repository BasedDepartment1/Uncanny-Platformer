using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float trapDamage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().ReduceHealthPoints(trapDamage);
        }
    }
}
