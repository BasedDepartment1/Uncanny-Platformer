namespace Source.Interfaces
{
    public interface IControls : ISwitchable
    {
        bool IsRightPressed { get; }

        bool IsLeftPressed { get; }

        bool IsJumpPressed { get; set; }
        
        bool IsRangedAttackPressed { get; set; }
    }
}