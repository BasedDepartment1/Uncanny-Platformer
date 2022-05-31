using System;

namespace Source.Interfaces
{
    public interface IJump
    {
        event Action<float> PerformJump;
    }
}