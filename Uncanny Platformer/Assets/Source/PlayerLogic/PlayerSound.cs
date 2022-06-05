using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerSound : MonoBehaviour
    {
        private string currentSound;
        private AudioManager manager;
        
        private IPlayer Player { get; set; }

        private Action<float> onJump;
        private Action onThrow;
        private Action onHurt;

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            manager = FindObjectOfType<AudioManager>();
            SetUpEvents();
            Player.Health.Death += OnDeath;
            Player.Respawn.Respawn += SetUpEvents;
        }

        private void SetUpEvents()
        {
            onJump = _ => ChangeSound("PlayerJump");
            onThrow = () => ChangeSound("ThrowSword");
            onHurt = () => ChangeSound("PlayerHurt");
            Player.Health.HpChanged += onHurt;
            Player.Jump.PerformJump += onJump;
            Player.RangedCombat.Throw += onThrow;
        }
        
        private void OnDeath()
        {
            ChangeSound("PlayerDeath");
            enabled = false;
            Player.Jump.PerformJump -= onJump;
            Player.RangedCombat.Throw -= onThrow;
            Player.Health.HpChanged -= onHurt;
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