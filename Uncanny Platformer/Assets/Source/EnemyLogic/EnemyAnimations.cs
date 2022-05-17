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
        [SerializeField] private Enemy enemy;

        private bool isHurting;
        private string currentState;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (enemy.health.IsDead)
            {
                ChangeAnimationState(EnemyAnimationStates.Die);
                return;
            }
        
            TryPlayHurtAnimation();

            if (!isHurting)
            {
                ChangeAnimationState(enemy.movement.isMoving 
                    ? EnemyAnimationStates.Move 
                    : EnemyAnimationStates.Idle);
            }
        }

        private void TryPlayHurtAnimation()
        {
            if (!enemy.health.WasHurt) return;
            
            enemy.health.WasHurt = false;
            if (!isHurting)
            {
                isHurting = true;
                ChangeAnimationState(EnemyAnimationStates.Hurt);
                Invoke(nameof(StopHurting),
                    animator.GetCurrentAnimatorStateInfo(0).length);
            }
            return;
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