using UnityEngine;

namespace Source.PlayerLogic
{
    internal struct AnimStates
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string Jump = "jump";
        public const string Fall = "Fall";
        public const string Throw = "ThrowWeapon";
        public const string Death = "Death";
        public const string Hurt = "Hurt";
    }
    
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
            if (player.health.IsDead)
            {
                ChangeAnimationState(AnimStates.Death);
                return;
            }
        
            if (player.health.WasHurt)
            {
                player.health.WasHurt = false;
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

            if (player.rangedCombat.IsThrowStarted)
            {
                if (!isThrowing)
                {
                    isThrowing = true;
                    ChangeAnimationState(AnimStates.Throw);
                    Invoke(nameof(EndThrow),
                        animator.GetCurrentAnimatorStateInfo(0).length * rangedAttackDelay);
                }
                player.rangedCombat.IsThrowStarted = false;
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
}