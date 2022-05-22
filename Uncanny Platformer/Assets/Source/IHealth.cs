using System;

namespace Source
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        
        bool IsDead { get; set; }

        event Action HpChanged;

        event Action Death;

        void ReduceHealthPoints(float damage);
    }
}