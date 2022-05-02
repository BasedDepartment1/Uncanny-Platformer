using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        var body = other.GetComponent<Rigidbody2D>();
        
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }
}
