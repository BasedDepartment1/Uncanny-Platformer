using System;

namespace Source.PlayerLogic
{
    public interface IMovement : ISwitchable
    {
        event Action<Directions> Move;

        event Action Idle;
    }
}