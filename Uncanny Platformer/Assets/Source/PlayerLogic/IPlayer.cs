using UnityEngine;

namespace Source.PlayerLogic
{
    public interface IPlayer
    {
        Rigidbody2D Body { get; }
        IControls Controls { get; }
        IMovement Movement { get; }
        IJump Jump { get; }
        IRangedCombat RangedCombat { get; }
        IHealth Health { get; }
        
        bool IsGrounded();
    }
}