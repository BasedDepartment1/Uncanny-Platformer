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

        private string currentState;
        private bool isThrowing;
        private bool isHurting;
    
        private Animator animator;
    
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            if (player.health.IsDead)
            {
                ChangeAnimationState(AnimStates.Death);
                return;
            }
            
            PlayWithoutDisruption(ref player.health.WasHurt, 
                ref isHurting, 
                AnimStates.Hurt, 
                nameof(StopHurting));
            
            if (!isThrowing && !isHurting && player.IsGrounded())
            {
                ChangeAnimationState(player.movement.IsRunning ? AnimStates.Run : AnimStates.Idle);
            }

            PlayVerticalMovementAnimation(player.Body.velocity);

            PlayWithoutDisruption(ref player.rangedCombat.IsThrowStarted,
                ref isThrowing,
                AnimStates.Throw,
                nameof(EndThrow),
                rangedAttackDelay);
        }

        private void PlayWithoutDisruption(
            ref bool needsStart, 
            ref bool isPlaying, 
            string animationName,
            string stopActionName,
            float delay = 1f
            )
        {
            if (!needsStart) return;
            
            if (!isPlaying)
            {
                isPlaying = true;
                ChangeAnimationState(animationName);
                Invoke(stopActionName,
                    animator.GetCurrentAnimatorStateInfo(0).length * delay);
            }
            needsStart = false;
        }

        private void PlayVerticalMovementAnimation(Vector2 velocity)
        {
            if (player.jump.IsJumping)
            {
                if (velocity.y > 0)
                {
                    ChangeAnimationState(AnimStates.Jump);
                }

                player.jump.IsJumping = false;
            }

            if (!player.IsGrounded() && velocity.y < 0)
            {
                ChangeAnimationState(AnimStates.Fall);
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