using System;

namespace Source.PlayerLogic
{
    public interface IRespawnable
    {
        event Action Respawn;

        void SetSpawn(SpawnPoint spawnPoint);
    }
}