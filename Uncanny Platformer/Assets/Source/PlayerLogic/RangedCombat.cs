using System;
using UnityEngine;

namespace Source.PlayerLogic
{
    public class RangedCombat : MonoBehaviour, IRangedCombat
    {
        [SerializeField] private Player player;
    
        [SerializeField] private float fireCooldown;
        [SerializeField] private float lifeTime;
        [SerializeField] private float knifeSpeed;
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject throwingKnife;
        [SerializeField] private AnimationClip throwingAnimation;
        [SerializeField] private float animationDelay = 0.6f;

        private float fireTimer = float.MaxValue;

        public event Action Throw;

        private void Start()
        {
            Throw += ThrowWeapon;
        }

        private void Update()
        {
            fireTimer += Time.deltaTime;
            if (!player.controls.IsRangedAttackPressed) return;
            player.controls.IsRangedAttackPressed = false;
            
            Throw();
        }

        private void ThrowWeapon()
        {
            if (!player.IsGrounded()
                || fireTimer <= fireCooldown) return;
            
            Invoke(nameof(Fire), throwingAnimation.length * animationDelay);
            fireTimer = 0;
        }

        private void Fire()
        {
            var knife = Instantiate(throwingKnife);
            knife.transform.position = firePosition.position;
            knife.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Mathf.Sign(transform.localScale.x) * knifeSpeed, 0f);
            Destroy(knife, lifeTime);
        }
    }
}
