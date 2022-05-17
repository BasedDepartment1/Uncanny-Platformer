using UnityEngine;

namespace Source.EnemyLogic
{
    internal struct EnemyAnimationStates
    {
        public static readonly string Idle = "Idle";
        public static readonly string Move = "Move";
        public static readonly string Hurt = "Hurt";
        public static readonly string Die = "Die";
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
        
            if (enemy.health.WasHurt)
            {
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

            if (!isHurting)
            {
                ChangeAnimationState(enemy.movement.isMoving 
                    ? EnemyAnimationStates.Move 
                    : EnemyAnimationStates.Idle);
            }
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