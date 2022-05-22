using System;
using System.Collections;
using UnityEngine;

namespace Source
{
    public class Health : MonoBehaviour, IHealth
    {
        public float CurrentHealth { get; private set; }
    
        [Header("Health")]
        [SerializeField] private float startingHealth = 100f;
    
        [Header("Invisibility")]
        [SerializeField] private float invisibilityDuration;
        [SerializeField] private int blinkCount;

        [SerializeField] private int entityLayerIndex;
        [SerializeField] private int[] damagingLayerIndices;

        public bool IsDead { get; set; }
        internal bool WasHurt;

        private SpriteRenderer sprite;

        public event Action HpChanged;

        public event Action Death;

        private void Start()
        {
            CurrentHealth = startingHealth;
            sprite = GetComponent<SpriteRenderer>();
            HpChanged += CheckHp;
        }

        public void ReduceHealthPoints(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
            HpChanged();
        }

        private void CheckHp()
        {
            if (CurrentHealth > 0)
            {
                StartCoroutine(ActivateInvisibility());
            }
            else
            {
                Death();
            }
        }

        // public void Revive()
        // {
        //     CurrentHealth = startingHealth;
        //     IsDead = false;
        // }
        

        private IEnumerator ActivateInvisibility()
        {
            SetIgnoreCollisions(true);
            for (var i = 0; i < blinkCount; i++)
            {
                var blinkTime = invisibilityDuration / (blinkCount * 2);
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
