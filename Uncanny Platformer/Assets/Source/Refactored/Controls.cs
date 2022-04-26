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
    [SerializeField] private KeyCode attackKey = KeyCode.E;
    
    internal bool IsRightPressed;
    internal bool IsLeftPressed;
    internal bool isJumpPressed;

    internal bool isMeleeAttackPressed;
    internal bool isRangedAttackPressed;

    private void CheckOnMoves()
    {
        IsLeftPressed = Input.GetKey(moveLeftKey);
        IsRightPressed = Input.GetKey(moveRightKey);
        if (Input.GetKeyDown(jumpKey))
        {
            isJumpPressed = true;
        }

        // isJumpPressed = Input.GetKeyDown(jumpKey);
    }

    private void CheckOnCombatActions()
    {
        if (Input.GetKeyDown(attackKey))
        {
            isMeleeAttackPressed = true;
        }
        if (Input.GetKeyDown(throwKey))
        {
            isRangedAttackPressed = true;
        }
        // isMeleeAttackPressed = Input.GetKeyDown(attackKey);
        // isRangedAttackPressed = Input.GetKeyDown(throwKey);
    }

    private void Update()
    {
        CheckOnMoves();
        CheckOnCombatActions();
    }

    // private PlayerMovement movement;
    //
    // private Combat combat;
    //
    // private MeleeAttack attack;
    // Start is called before the first frame update
    // void Awake()
    // {
    //     movement = GetComponent<PlayerMovement>();
    //     combat = GetComponent<Combat>();
    //     attack = GetComponent<MeleeAttack>();
    // }

    // void Update()
    // {
    //     CheckOnMoves();
    //     CheckOnCombatActions();
    // }

    // private void CheckOnMoves()
    // {
    //     if (Input.GetKey(moveLeftKey))
    //     {
    //         movement.MoveToDirection(Directions.Left);
    //     }
    //     else if (Input.GetKey(moveRightKey))
    //     {
    //         movement.MoveToDirection(Directions.Right);
    //     }
    //     else
    //     {
    //         movement.MoveToDirection(Directions.None);
    //     }
    //     
    //     if (Input.GetKeyDown(jumpKey))
    //     {
    //         movement.Jump();
    //     }
    // }
    //
    // private void CheckOnCombatActions()
    // {
    //     if (Input.GetKeyDown(attackKey))
    //     {
    //         attack.Attack();
    //     }
    //     if (Input.GetKeyDown(throwKey))
    //     {
    //         combat.Fire();
    //     }
    // }
}
