using System;

namespace Source.Interfaces
{
    public interface IHealth
    {
        bool IsDead { get; }
        
        event Action HpChanged;

        event Action Death;
    }
}