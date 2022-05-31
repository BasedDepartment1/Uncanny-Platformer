using Source.PlayerLogic;

namespace Source.Interfaces
{
    public interface IPlayer : IMainComponent
    {
        IControls Controls { get; }
        IMovement Movement { get; }
        IJump Jump { get; }
        IRangedCombat RangedCombat { get; }
        IHealth Health { get; }
        IRespawnable Respawn { get; }
        
        bool IsGrounded();
    }
}