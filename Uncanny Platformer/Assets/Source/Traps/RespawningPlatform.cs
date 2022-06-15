using Source.PlayerLogic;
using UnityEngine;

namespace Source.Traps
{
    public class RespawningPlatform : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Transform respawnPoint;
        [SerializeField] private GameObject platform;
        
        private GameObject oldPlatform;
        
        private void Start()
        {
            player.Respawn.Respawn += OnRespawn;
        }

        private void OnRespawn()
        {
            var newPlatform = Instantiate(platform);
            newPlatform.transform.position = (Vector2) respawnPoint.position;
            oldPlatform = platform;
            platform = newPlatform;
            Invoke(nameof(RemoveOldPlatform), player.Respawn.DeathTime);
            
        }

        private void RemoveOldPlatform()
        {
            Destroy(oldPlatform);
        }
    }
}
