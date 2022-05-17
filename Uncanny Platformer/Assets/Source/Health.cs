using System.Collections;
using UnityEngine;

namespace Source
{
    public class Health : MonoBehaviour
    {
        public float CurrentHealth { get; private set; }
    
        [Header("Health")]
        [SerializeField] private float startingHealth = 100f;
    
        [Header("Invisibility")]
        [SerializeField] private float invisibilityDuration;
        [SerializeField] private int blinkCount;

        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask enemyLayer;

        internal bool IsDead;
        internal bool WasHurt;

        private SpriteRenderer sprite;

        private void Start()
        {
            CurrentHealth = startingHealth;
            sprite = GetComponent<SpriteRenderer>();
        }

        public void ReduceHealthPoints(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
            if (CurrentHealth > 0)
            {
                StartCoroutine(ActivateInvisibility());
                WasHurt = true;
            }
            else
            {
                IsDead = true;
            }
        }

        internal void Revive()
        {
            CurrentHealth = startingHealth;
            IsDead = false;
        }

        private IEnumerator ActivateInvisibility()
        {
            Physics2D.IgnoreLayerCollision(MaskToLayer(playerLayer),
                MaskToLayer(enemyLayer), true);
            for (var i = 0; i < blinkCount; i++)
            {
                var blinkTime = invisibilityDuration / (blinkCount * 2);
                sprite.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(blinkTime);
                sprite.color = Color.white;
                yield return new WaitForSeconds(blinkTime);
            }
            Physics2D.IgnoreLayerCollision(MaskToLayer(playerLayer), 
                MaskToLayer(enemyLayer), false);
        }

        private int MaskToLayer(LayerMask mask)
        {
            var layerNumber = 0;
            while (mask.value > 1)
            {
                mask.value >>= 1;
                layerNumber++;
            }

            return layerNumber;
        }
    }
}
