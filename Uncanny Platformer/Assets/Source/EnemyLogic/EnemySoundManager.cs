using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemySoundManager : MonoBehaviour
    {
        private IEnemy Enemy { get; set; }

        private AudioManager manager;
        private string currentSound;

        private void Start()
        {
            Enemy = GetComponent<IEnemy>();
            manager = FindObjectOfType<AudioManager>();
            Enemy.PatrolBehaviour.Move += OnMove;
        }
        
        private void OnMove(Directions _)
        {
            ChangeSound("SlimeMove");
        }
        
        private void ChangeSound(string newSound)
        {
            if (newSound == currentSound) return;

            currentSound = newSound;
            manager.Play(currentSound);
        }
    }
}