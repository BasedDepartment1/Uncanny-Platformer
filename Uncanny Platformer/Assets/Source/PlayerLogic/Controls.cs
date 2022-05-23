using UnityEngine;

namespace Source.PlayerLogic
{
    public class Controls : MonoBehaviour, IControls
    {
        [Header("Control scheme")]
        [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
        [SerializeField] private KeyCode moveRightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.W;
        [SerializeField] private KeyCode throwKey = KeyCode.R;
    
        public bool IsRightPressed { get; set; }
        public bool IsLeftPressed { get; set; }
        public bool IsJumpPressed { get; set; }
        public bool IsRangedAttackPressed { get; set; }

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
            if (!enabled) return;
            
            CheckOnMoves();
            CheckOnCombatActions();
        }

        public void Switch(bool mode)
        {
            enabled = mode;
        }
    }
}
