using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    enum SpawnAnimations
    {
        Active,
        Inactive
    }
    
    public class SpawnPoint : MonoBehaviour, ISwitchable
    { 
        public Vector2 Position => transform.position;

        private Animator animator;

        public void Switch(bool mode)
        {
            animator.Play(mode 
                ? SpawnAnimations.Active.ToString() 
                : SpawnAnimations.Inactive.ToString());
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IRespawnable>()?.SetSpawn(this);
        }
    }
}