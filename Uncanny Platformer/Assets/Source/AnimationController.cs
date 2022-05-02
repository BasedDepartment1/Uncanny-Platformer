using System;
using Source.Refactored;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float rangedAttackDelay = 0.7f;
    private readonly AnimStates animStates;

    private string currentState;
    private bool isThrowing;
    private bool isHurting;
    

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health.isDead)
        {
            ChangeAnimationState(AnimStates.Death);
            return;
        }
        
        if (player.health.wasHurt)
        {
            player.health.wasHurt = false;
            if (!isHurting)
            {
                isHurting = true;
                ChangeAnimationState(AnimStates.Hurt);
                Invoke(nameof(StopHurting),
                    animator.GetCurrentAnimatorStateInfo(0).length);
            }
            return;
        }
        
        
        if (!isThrowing && !isHurting && player.IsGrounded())
        {
            ChangeAnimationState(player.movement.isRunning ? AnimStates.Run : AnimStates.Idle);
        }
        
        var velocity = player.body.velocity;
        
        if (player.movement.isJumping)
        {
            if (velocity.y > 0)
            {
                ChangeAnimationState(AnimStates.Jump);
            }

            player.movement.isJumping = false;
        }

        if (!player.IsGrounded() && velocity.y < 0)
        {
            ChangeAnimationState(AnimStates.Fall);
        }

        if (player.rangedCombat.isThrowStarted)
        {
            if (!isThrowing)
            {
                isThrowing = true;
                ChangeAnimationState(AnimStates.Throw);
                Invoke(nameof(EndThrow),
                    animator.GetCurrentAnimatorStateInfo(0).length * rangedAttackDelay);
            }
            player.rangedCombat.isThrowStarted = false;
        }
    }

    private void StopHurting()
    {
        isHurting = false;
    }
    
    private void EndThrow()
    {
        isThrowing = false;
    }

    private void ChangeAnimationState(string newState)
    {
        if (newState == currentState)
        {
            return;
        }
        currentState = newState;
        animator.Play(currentState);
    }
}
