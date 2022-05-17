using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float rotationSpeed = 2f;
    void Update()
    {
        transform.Rotate(0f, 0f, 360 * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        other.gameObject.GetComponent<Health>().ReduceHealthPoints(damage);
    }
}
