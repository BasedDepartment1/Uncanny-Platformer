using System;
using System.Collections;
using Source.Interfaces;
using UnityEngine;

namespace Source
{
    public class Health : MonoBehaviour, IHealth, IDamageable
    {
        public float CurrentHealth { get; private set; }
    
        [Header("Health")]
        [SerializeField] private float startingHealth = 100f;
    
        [Header("Invisibility")]
        [SerializeField] private float invisibilityDuration;
        [SerializeField] private int blinkCount;

        [SerializeField] private int entityLayerIndex;
        [SerializeField] private int[] damagingLayerIndices;

        private SpriteRenderer sprite;
        private bool isDead;

        public event Action HpChanged;

        public event Action Death;

        public void Damage(float damage)
        {
            for (var i = 0; i < 10; i++)
            {
                ReduceHealthPoints(damage / 10);
            }
        }

        public void Kill()
        {
            for (var i = 0; i < 100; i++)
            {
                ReduceHealthPoints(startingHealth / 100);
            }
        }

        private void Start()
        {
            CurrentHealth = startingHealth;
            sprite = GetComponent<SpriteRenderer>();
            HpChanged += CheckHp;
            Death += () => isDead = true;
            
            var respawn = GetComponent<IRespawnable>();
            if (respawn != null)
            {
                respawn.Respawn += OnRespawn;
            }
        }

        private void ReduceHealthPoints(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
            HpChanged?.Invoke();
        }

        private void CheckHp()
        {
            if (isDead) return;
            
            if (CurrentHealth > 1e-6)
            {
                StartCoroutine(ActivateInvisibility());
            }
            else
            {
                Death?.Invoke();
            }
        }

        private void OnRespawn()
        {
            CurrentHealth = startingHealth;
            isDead = false;
        }

        private IEnumerator ActivateInvisibility()
        {
            SetIgnoreCollisions(true);
            var blinkTime = invisibilityDuration / (blinkCount * 2);
            for (var i = 0; i < blinkCount; i++)
            {
                sprite.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(blinkTime);
                sprite.color = Color.white;
                yield return new WaitForSeconds(blinkTime);
            }
            SetIgnoreCollisions(false);
        }

        private void SetIgnoreCollisions(bool mode)
        {
            foreach (var layerIndex in damagingLayerIndices)
            {
                Physics2D.IgnoreLayerCollision(entityLayerIndex, layerIndex, mode);
            }
        }
    }
}
