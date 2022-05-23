namespace Source.PlayerLogic
{
    public interface IPlayer : IMainComponent
    {
        IControls Controls { get; }
        IMovement Movement { get; }
        IJump Jump { get; }
        IRangedCombat RangedCombat { get; }
        IHealth Health { get; }
        
        bool IsGrounded();
    }
}