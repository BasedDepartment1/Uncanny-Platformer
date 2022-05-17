using Source.PlayerLogic;
using UnityEngine;

namespace Source.Traps
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }
        
            var playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.ActivateJump(jumpForce);
            // body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
    }
}
