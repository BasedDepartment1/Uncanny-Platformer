using System;
using UnityEngine;

namespace Source.EnemyLogic
{
    public class EnemySound : MonoBehaviour
    {
        private IEnemy Enemy { get; set; }

        private AudioManager manager;
        private string currentSound;

        private Action onHurt;

        private void Start()
        {
            Enemy = GetComponent<IEnemy>();
            manager = FindObjectOfType<AudioManager>();
            SetUpEvents();
            Enemy.Health.HpChanged += onHurt;
            Enemy.Health.Death += OnDeath;
        }
        
        private void SetUpEvents()
        {
            onHurt = () => ChangeSound("SlimeHurt");
        }

        private void OnDeath()
        {
            ChangeSound("SlimeDeath");
            enabled = false;
            Enemy.Health.HpChanged -= onHurt;
            Enemy.Health.Death -= OnDeath;
        }
        private void ChangeSound(string newSound)
        {
            if (newSound == currentSound) return;
        
            currentSound = newSound;
            manager.Play(currentSound, out var length);
            Invoke(nameof(Refresh), length);
        }

        private void Refresh() => currentSound = null;
    }
}