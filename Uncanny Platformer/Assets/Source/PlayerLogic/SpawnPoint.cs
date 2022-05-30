using UnityEngine;

namespace Source.PlayerLogic
{
    internal enum SpawnAnimations
    {
        Active,
        Inactive
    }
    
    public class SpawnPoint : MonoBehaviour, ISwitchable
    { 
        [SerializeField] private Transform point;
        
        public Vector2 Position => point.transform.position;

        private bool isActive;
        private Animator animator;

        public void Switch(bool mode)
        {
            isActive = mode;
            animator.Play(mode 
                ? SpawnAnimations.Active.ToString() 
                : SpawnAnimations.Inactive.ToString());

            if (mode)
            {
                //TODO play sound 
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.Play(isActive 
                ? SpawnAnimations.Active.ToString() 
                : SpawnAnimations.Inactive.ToString());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<IRespawnable>()?.SetSpawn(this);
        }
    }
}