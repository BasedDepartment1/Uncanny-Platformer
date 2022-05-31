using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemyPatrol : MonoBehaviour, IMovement
    {
        [Header("Patrol Objects")] 
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;
        [SerializeField] private Enemy enemy;

        [Header("Patrol Timings")]
        [SerializeField] private float standDuration = 1;

        [SerializeField] private float speed = 5f;

        private bool movingRight;
        private float standTimer;

        public event Action<Directions> Move;
        public event Action Idle;

        private void Start()
        {
            Move += StartMoving;
            Idle += StandStill;
        }

        private void FixedUpdate()
        {
            if (!enabled) return;
            if (movingRight)
            {
                if (enemy.transform.position.x < rightEdge.position.x)
                {
                    Move(Directions.Right);
                }
                else
                {
                    Idle();
                }
            }
            else
            {
                if (enemy.transform.position.x >= leftEdge.position.x)
                {
                    Move(Directions.Left);
                }
                else
                {
                    Idle();
                }
            }
        }

        private void StandStill()
        {
            standTimer += Time.deltaTime;
            if (standTimer > standDuration)
            {
                movingRight = !movingRight;
            }
        }

        private void StartMoving(Directions direction)
        {
            standTimer = 0;
            MoveToDirection(direction);
        }

        private void MoveToDirection(Directions direction)
        {
            var initialScale = enemy.transform.localScale;
            enemy.transform.localScale = new Vector3(-Mathf.Abs(initialScale.x) * (int)direction, 
                initialScale.y, initialScale.z);
            enemy.Body.velocity = new Vector2((int)direction * speed, enemy.Body.velocity.y);
        }

        public void Switch(bool mode)
        {
            enabled = mode;
        }
    }
}
