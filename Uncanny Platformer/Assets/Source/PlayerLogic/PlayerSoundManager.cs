using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerSoundManager : MonoBehaviour
    {
        private bool isMoving;
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
            Player.Jump.PerformJump += onJump;
            Player.RangedCombat.Throw += onThrow;
            Player.Health.Death += OnDeath;
            Player.Health.HpChanged += onHurt;

        }

        private void SetUpEvents()
        {
            onJump = _ => ChangeSound("PlayerJump");
            onThrow = () => ChangeSound("ThrowSword");
            onHurt = () => ChangeSound("PlayerHurt");
        }
        
        private void OnDeath()
        {
            ChangeSound("PlayerDeath");
            enabled = false;
            Player.Jump.PerformJump -= onJump;
            Player.RangedCombat.Throw -= onThrow;
            Player.Health.HpChanged -= onHurt;
            Player.Health.Death -= OnDeath;
        }

        private void ChangeSound(string newSound)
        {
            //if (newSound == currentSound) return;
            currentSound = newSound;
            manager.Play(currentSound, out var length);
            Invoke(nameof(Refresh), length);
        }

        private void Refresh() => currentSound = null;

        private void StopSound()
        {
            manager.Stop(currentSound);
        }
    }

}