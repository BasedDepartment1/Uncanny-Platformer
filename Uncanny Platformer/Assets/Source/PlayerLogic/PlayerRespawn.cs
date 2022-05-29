using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerRespawn :  MonoBehaviour, IRespawnable
    {
        public event Action Respawn;
        
        [SerializeField] private float deathTime = 2;
        [SerializeField] private SpawnPoint currentSpawnPoint;

        private IPlayer Player { get; set; }


        public void SetSpawn(SpawnPoint spawnPoint)
        {
            currentSpawnPoint.Switch(false);
            currentSpawnPoint = spawnPoint;
            currentSpawnPoint.Switch(true);
        }

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            Player.Health.Death += OnDeath;
            Respawn += OnRespawn;
            currentSpawnPoint.Switch(true);
        }

        private void OnDeath()
        {
            Invoke(nameof(Respawn), deathTime);
        }

        private void OnRespawn()
        {
            Player.Body.position = currentSpawnPoint.Position;
        }
    }
}