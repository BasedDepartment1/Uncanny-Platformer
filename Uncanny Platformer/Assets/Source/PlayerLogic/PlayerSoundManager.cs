using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class PlayerSoundManager : MonoBehaviour
    {
        private string currentSound;
        private AudioManager manager;
        
        private IPlayer Player { get; set; }

        private Action<float> onJump;
        private Action onThrow;

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            manager = FindObjectOfType<AudioManager>();
            SetUpEvents();
            Player.Jump.PerformJump += onJump;
            Player.RangedCombat.Throw += onThrow;
            Player.Health.Death += OnDeath;

        }

        private void SetUpEvents()
        {
            onJump = _ => ChangeSound("PlayerJump");
            onThrow = () => ChangeSound("ThrowSword");
        }

        private void OnDeath()
        {
            ChangeSound("PlayerDeath");
            Player.Jump.PerformJump -= onJump;
            Player.RangedCombat.Throw -= onThrow;
        }

        private void ChangeSound(string newSound)
        {
            if (newSound == currentSound) return;
        
            currentSound = newSound;
            manager.Play(currentSound);
        }
    }

}