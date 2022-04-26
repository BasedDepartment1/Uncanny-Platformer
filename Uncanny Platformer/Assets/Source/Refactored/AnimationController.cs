using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Jump = "jump";
    private const string Fall = "Fall";
    private const string Throw = "ThrowWeapon";
    private const string Death = "Death";
    private const string Hurt = "Hurt";

    private string currentState;
    private bool isAttacking;
    private bool isThrowing;
    private bool isHurting;
    

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.health.isDead)
        {
            if (!player.health.wasHurt)
            {
                var velocity = player.body.velocity;
                if (!isThrowing && !isAttacking && !isHurting)
                {
                    if (player.IsGrounded() && !isAttacking)
                    {
                        ChangeAnimationState(player.movement.isRunning ? Run : Idle);
                    }
                }

                if (player.movement.isJumping)
                {
                    if (velocity.y > 0)
                    {
                        ChangeAnimationState(Jump);
                    }

                    player.movement.isJumping = false;
                }

                if (!player.IsGrounded() && velocity.y < 0)
                {
                    ChangeAnimationState(Fall);
                }

                if (player.rangedCombat.isThrowStarted)
                {
                    player.rangedCombat.isThrowStarted = false;
                    if (!isThrowing)
                    {
                        isThrowing = true;
                        ChangeAnimationState(Throw);
                        Invoke(nameof(EndThrow),
                            animator.GetCurrentAnimatorStateInfo(0).length * 0.7f);
                    }
                }
            }
            else
            {
                player.health.wasHurt = false;
                if (!isHurting)
                {
                    isHurting = true;
                    ChangeAnimationState(Hurt);
                    Invoke(nameof(StopHurting),
                        animator.GetCurrentAnimatorStateInfo(0).length);
                }
            }
        }
        else
        {
            ChangeAnimationState(Death);
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
        // Debug.Log("Playing animation " + newState);
        currentState = newState;
        animator.Play(currentState);
    }
}
