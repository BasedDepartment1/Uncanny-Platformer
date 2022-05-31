using System;
using Source.Interfaces;
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
        [SerializeField] private float rangedAttackDelay = 0.7f;

        private string currentState;
        private bool isThrowing;
        private bool isHurting;
    
        private Animator animator;
        
        private IPlayer Player { get; set; }

        private Action<float> onJump;
        private Action onThrow;
        private Action onHpChanged;
        private Action<Directions> onMove;

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            animator = GetComponent<Animator>();
            SetUpEvents();
            Player.Health.Death += OnDeath;
            Player.Respawn.Respawn += OnRespawn;
        }

        private void Update()
        {
            if (!enabled) return;
            
            PlayFall(Player.Body.velocity);
        }

        private void SetUpEvents()
        {
            onMove = _ => PlayMove();
            
            onJump = _ => PlayJump();
            
            onThrow = () => PlayWithoutDisruption(
                ref isThrowing,
                AnimStates.Throw,
                nameof(EndThrow),
                rangedAttackDelay);
            
            onHpChanged = () => PlayWithoutDisruption(
                    ref isHurting,
                    AnimStates.Hurt,
                    nameof(StopHurting));

            Player.Movement.Move += onMove;
            Player.Movement.Idle += OnIdle;
            Player.Jump.PerformJump += onJump;
            Player.RangedCombat.Throw += onThrow;
            Player.Health.HpChanged += onHpChanged;
        }

        private void OnIdle()
        {
            if (isThrowing || isHurting || !Player.IsGrounded()) return;
            
            ChangeAnimationState(AnimStates.Idle);
        }

        private void OnDeath()
        {
            Player.Movement.Move -= onMove;
            Player.Movement.Idle -= OnIdle;
            Player.Jump.PerformJump -= onJump;
            Player.RangedCombat.Throw -= onThrow;
            Player.Health.HpChanged -= onHpChanged;
            enabled = false;
            ChangeAnimationState(AnimStates.Death);
        }

        private void PlayMove()
        {
            if (isThrowing || isHurting || !Player.IsGrounded()) return;
            
            ChangeAnimationState(AnimStates.Run);
        }

        private void OnRespawn()
        {
            SetUpEvents();
            enabled = true;
        }

        private void PlayWithoutDisruption(
            ref bool isPlaying, 
            string animationName,
            string stopActionName,
            float delay = 1f
        )
        {
            if (isPlaying) return;
            
            isPlaying = true;
            ChangeAnimationState(animationName);
            Invoke(stopActionName,
                animator.GetCurrentAnimatorStateInfo(0).length * delay);
        }

        private void PlayFall(Vector2 velocity)
        {
            if (!Player.IsGrounded() && velocity.y < 0)
            {
                ChangeAnimationState(AnimStates.Fall);
            }
        }

        private void PlayJump()
        {
            if (Player.Body.velocity.y > 0)
            {
                ChangeAnimationState(AnimStates.Jump);
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
            if (newState == currentState) return;
            
            currentState = newState;
            animator.Play(currentState);
        }
    }
}