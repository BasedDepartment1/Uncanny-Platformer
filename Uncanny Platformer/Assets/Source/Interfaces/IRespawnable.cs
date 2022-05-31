using System;
using Source.PlayerLogic;
using Source.Traps;

namespace Source.Interfaces
{
    public interface IRespawnable
    {
        event Action Respawn;

        void SetSpawn(SpawnPoint spawnPoint);
    }
}