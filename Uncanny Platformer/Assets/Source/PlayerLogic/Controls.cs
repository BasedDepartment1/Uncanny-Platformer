using System;
using System.Linq;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class Controls : MonoBehaviour, IControls
    {
        [Header("Control scheme")]
        [SerializeField] private KeyCode[] moveLeftKeys 
            = {KeyCode.A, KeyCode.LeftArrow};
        [SerializeField] private KeyCode[] moveRightKeys 
            = {KeyCode.D, KeyCode.RightArrow};
        [SerializeField] private KeyCode[] jumpKeys 
            = {KeyCode.W, KeyCode.Space, KeyCode.UpArrow};
        [SerializeField] private KeyCode[] throwKeys 
            = {KeyCode.R};
    
        public bool IsRightPressed { get; private set; }
        public bool IsLeftPressed { get; private set; }
        public bool IsJumpPressed { get; set; }
        public bool IsRangedAttackPressed { get; set; }

        private void CheckOnMoves()
        {
            IsRightPressed = AreBeingPressed(moveRightKeys);
            IsLeftPressed = AreBeingPressed(moveLeftKeys);
            IsJumpPressed = WerePressed(jumpKeys) || IsJumpPressed;
        }

        private void CheckOnCombatActions()
        {
            IsRangedAttackPressed = WerePressed(throwKeys) || IsRangedAttackPressed;
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

        private bool AreBeingPressed(KeyCode[] keys) => AreActionButtonsPressed(keys, Input.GetKey);

        private bool WerePressed(KeyCode[] keys) => AreActionButtonsPressed(keys, Input.GetKeyDown);

        private bool AreActionButtonsPressed(KeyCode[] keys, Func<KeyCode, bool> method)
        {
            return keys.Any(method);
        }
    }
}
