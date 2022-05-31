using System;

namespace Source.Interfaces
{
    public interface IHealth
    {
        event Action HpChanged;

        event Action Death;
    }
}