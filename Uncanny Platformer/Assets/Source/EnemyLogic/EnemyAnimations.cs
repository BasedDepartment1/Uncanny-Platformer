using UnityEngine;

namespace Source.EnemyLogic
{
    internal struct EnemyAnimationStates
    {
        public const string Idle = "Idle";
        public const string Move = "Move";
        public const string Hurt = "Hurt";
        public const string Die = "Die";
    }

    public class EnemyAnimations : MonoBehaviour
    {
        // [SerializeField] private Enemy enemy;
        
        private bool isHurting;
        private string currentState;
        private Animator animator;
        
        private IEnemy Enemy { get; set; }

        private void Start()
        {
            Enemy = GetComponent<IEnemy>();
            animator = GetComponent<Animator>();
            Enemy.PatrolBehaviour.Move += OnMove;
            Enemy.PatrolBehaviour.Idle += OnIdle;
            Enemy.Health.HpChanged += OnHurt;
            Enemy.Health.Death += OnDeath;
        }

        private void OnDeath()
        {
            ChangeAnimationState(EnemyAnimationStates.Die);
            enabled = false;
            Enemy.PatrolBehaviour.Move -= OnMove;
            Enemy.PatrolBehaviour.Idle -= OnIdle;
            Enemy.Health.HpChanged -= OnHurt;
            Enemy.Health.Death -= OnDeath;
        }

        private void OnMove(Directions _)
        {
            if (isHurting) return;
            
            ChangeAnimationState(EnemyAnimationStates.Move);
        }

        private void OnIdle()
        {
            if (isHurting) return;
            
            ChangeAnimationState(EnemyAnimationStates.Idle);
        }

        private void OnHurt()
        {
            if (isHurting) return;
            isHurting = true;
            ChangeAnimationState(EnemyAnimationStates.Hurt);
            Invoke(nameof(StopHurting),
                animator.GetCurrentAnimatorStateInfo(0).length);
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

        private void StopHurting()
        {
            isHurting = false;
        }
    }
}