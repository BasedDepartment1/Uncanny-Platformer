using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private KeyCode throwKey;
    [SerializeField] private KeyCode attackKey;
    
    private PlayerMovement movement;

    private Combat combat;

    private MeleeAttack attack;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<Combat>();
        attack = GetComponent<MeleeAttack>();
    }
    
    void Update()
    {
        CheckOnMoves();
        CheckOnCombatActions();
    }

    private void CheckOnMoves()
    {
        if (Input.GetKey(moveLeftKey))
        {
            movement.MoveToDirection(Directions.Left);
        }
        else if (Input.GetKey(moveRightKey))
        {
            movement.MoveToDirection(Directions.Right);
        }
        else
        {
            movement.MoveToDirection(Directions.None);
        }
        
        if (Input.GetKeyDown(jumpKey))
        {
            movement.Jump();
        }
    }

    private void CheckOnCombatActions()
    {
        if (Input.GetKeyDown(attackKey))
        {
            attack.Attack();
        }
        if (Input.GetKeyDown(throwKey))
        {
            combat.Fire();
        }
    }
}
