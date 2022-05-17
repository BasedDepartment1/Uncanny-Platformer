using UnityEngine;

namespace Source.Traps
{
    public class MovingTrap : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        private int currentTargetIndex;

        [SerializeField] private float speed = 2f;

        private Transform CurrentTarget => waypoints[currentTargetIndex];
    
        void Update()
        {
            if (Vector2.Distance(transform.position,
                CurrentTarget.position) < 0.1f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % waypoints.Length;
            }

            transform.position = Vector2.MoveTowards(transform.position,
                CurrentTarget.position,
                Time.deltaTime * speed);
        }
    }
}
