using System;

namespace Source.Interfaces
{
    public interface IMovement : ISwitchable
    {
        event Action<Directions> Move;

        event Action Idle;
    }
}