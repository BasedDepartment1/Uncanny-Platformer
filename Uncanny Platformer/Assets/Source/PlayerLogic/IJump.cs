using System;

namespace Source.PlayerLogic
{
    public interface IJump
    {
        void ActivateJump(float jumpForce);

        event Action<float> PerformJump;
    }
}