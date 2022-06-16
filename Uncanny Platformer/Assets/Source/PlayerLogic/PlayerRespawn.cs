using System;
using Source.Interfaces;
using Source.Traps;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerRespawn :  MonoBehaviour, IRespawnable
    {
        public event Action Respawn;
        
        [SerializeField] private float deathTime = 2;
        [SerializeField] private SpawnPoint currentSpawnPoint;
        
        public float DeathTime => deathTime;

        private IPlayer Player { get; set; }


        public void SetSpawn(SpawnPoint spawnPoint)
        {
            if (currentSpawnPoint.Position == spawnPoint.Position) return;
            
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
            Invoke(nameof(ActivateRespawn), deathTime);
        }

        private void OnRespawn()
        {
            Player.Body.position = currentSpawnPoint.Position;
        }

        private void ActivateRespawn()
        {
            transform.SetParent(null);
            Respawn?.Invoke();
        }
    }
}