using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    internal bool isJumpPressed;
    
    internal bool isRangedAttackPressed;

    private void CheckOnMoves()
    {
        IsLeftPressed = Input.GetKey(moveLeftKey);
        IsRightPressed = Input.GetKey(moveRightKey);
        isJumpPressed = Input.GetKeyDown(jumpKey) || isJumpPressed;
        // if (Input.GetKeyDown(jumpKey))
        // {
        //     isJumpPressed = true;
        // }
    }

    private void CheckOnCombatActions()
    {
        // if (Input.GetKeyDown(throwKey))
        // {
        //     isRangedAttackPressed = true;
        // }
        isRangedAttackPressed = Input.GetKeyDown(throwKey) || isRangedAttackPressed;
    }

    private void Update()
    {
        CheckOnMoves();
        CheckOnCombatActions();
    }
}
