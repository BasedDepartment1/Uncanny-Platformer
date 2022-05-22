using System;

namespace Source.PlayerLogic
{
    public interface IMovement
    {
        event Action<Directions> Move;

        event Action Idle;
    }
}