using System;

namespace Source
{
    public interface IHealth
    {
        event Action HpChanged;

        event Action Death;
    }
}