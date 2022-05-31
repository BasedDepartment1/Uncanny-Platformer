using System;

namespace Source.PlayerLogic
{
    public interface IJump
    {
        event Action<float> PerformJump;
    }
}