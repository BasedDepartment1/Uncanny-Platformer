using Source.PlayerLogic;
using UnityEngine;

namespace Source.Traps
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ITossable>() == null) return;
            
            other.GetComponent<ITossable>().Toss(jumpForce);
            FindObjectOfType<AudioManager>().Play("JumperSound");
        }
    }
}
