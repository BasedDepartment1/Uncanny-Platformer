using System;
using System.Collections;
using Source.PlayerLogic;
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

        public event Action HpChanged;

        public event Action Death;

        public void Damage(float damage)
        {
            ReduceHealthPoints(damage);
        }

        public void Kill()
        {
            ReduceHealthPoints(CurrentHealth * 10);
        }

        private void Start()
        {
            CurrentHealth = startingHealth;
            sprite = GetComponent<SpriteRenderer>();
            HpChanged += CheckHp;
            
            var respawn = GetComponent<IRespawnable>();
            if (respawn != null)
            {
                respawn.Respawn += () => CurrentHealth = startingHealth;
            }
        }

        private void ReduceHealthPoints(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
            HpChanged?.Invoke();
        }

        private void CheckHp()
        {
            if (CurrentHealth > 0)
            {
                StartCoroutine(ActivateInvisibility());
            }
            else
            {
                Death?.Invoke();
            }
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
