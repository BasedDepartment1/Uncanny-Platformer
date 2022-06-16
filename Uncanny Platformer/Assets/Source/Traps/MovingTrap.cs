using UnityEngine;

namespace Source.Traps
{
    public class MovingTrap : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        private int currentTargetIndex;

        [SerializeField] private float speed = 2f;

        private Transform CurrentTarget => waypoints[currentTargetIndex];
    
        private void Update()
        {
            if (!enabled) return;
            
            if (HasReachedCurrentPoint())
            {
                PickNextPoint();
            }

            MoveToNextPoint();
        }

        private bool HasReachedCurrentPoint()
        {
            return Vector2.Distance(
                transform.position,
                CurrentTarget.position) 
                   < 0.1f;
        }

        private void MoveToNextPoint(float speedModifier = 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                            CurrentTarget.position,
                            Time.deltaTime * speed * speedModifier);
        }

        private void PickNextPoint()
        {
            currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
        }
    }
}
