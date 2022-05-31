using Source.PlayerLogic;

namespace Source.EnemyLogic
{
    public interface IEnemy : IMainComponent
    {
        IHealth Health { get; }
        
        IMovement PatrolBehaviour { get; }
        
    }
}