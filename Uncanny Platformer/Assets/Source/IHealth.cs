using System;

namespace Source
{
    public interface IHealth
    {
        float CurrentHealth { get; }

        event Action HpChanged;

        event Action Death;
    }
}