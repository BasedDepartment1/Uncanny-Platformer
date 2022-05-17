using UnityEngine;

namespace Source.PlayerLogic
{
    public class Controls : MonoBehaviour
    {
        [Header("Player script")] [SerializeField]
        private Player player;
    
        [Header("Control scheme")]
        [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
        [SerializeField] private KeyCode moveRightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        [SerializeField] private KeyCode throwKey = KeyCode.R;
    
        internal bool IsRightPressed;
        internal bool IsLeftPressed;
        internal bool IsJumpPressed;
    
        internal bool IsRangedAttackPressed;

        private void CheckOnMoves()
        {
            IsLeftPressed = Input.GetKey(moveLeftKey);
            IsRightPressed = Input.GetKey(moveRightKey);
            IsJumpPressed = Input.GetKeyDown(jumpKey) || IsJumpPressed;
        }

        private void CheckOnCombatActions()
        {
            IsRangedAttackPressed = Input.GetKeyDown(throwKey) || IsRangedAttackPressed;
        }

        private void Update()
        {
            CheckOnMoves();
            CheckOnCombatActions();
        }
    }
}
