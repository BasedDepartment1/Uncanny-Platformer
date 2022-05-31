using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.PlayerLogic
{
    internal enum ThrowTypes
    {
        Normal = 1,
        Uncanny = -1
    }
    
    public class RangedCombat : MonoBehaviour, IRangedCombat
    {
        [SerializeField] private float fireCooldown;
        [SerializeField] private float lifeTime;
        [SerializeField] private float knifeSpeed;
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject throwingKnife;
        [SerializeField] private AnimationClip throwingAnimation;
        [SerializeField] private float animationDelay = 0.6f;

        [Header("For becoming uncanny")] [SerializeField]
        private ThrowTypes bias = ThrowTypes.Normal;

        private float fireTimer = float.MaxValue;

        private IPlayer Player { get; set; }
        
        public event Action Throw;

        private void Start()
        {
            Player = GetComponent<IPlayer>();
            Throw += ThrowWeapon;
        }

        private void Update()
        {
            fireTimer += Time.deltaTime;
            if (!Player.Controls.IsRangedAttackPressed 
                || !Player.IsGrounded()
                || fireTimer <= fireCooldown) return;
            
            Player.Controls.IsRangedAttackPressed = false;
            
            Throw();
        }

        private void ThrowWeapon()
        {
            Invoke(nameof(Fire), throwingAnimation.length * animationDelay);
            fireTimer = 0;
        }

        private void Fire()
        {
            var knife = Instantiate(throwingKnife);
            var scale = knife.transform.localScale;
            scale.x *= transform.localScale.x * (int)bias;
            knife.transform.localScale = scale;
            
            knife.transform.position = firePosition.position; 
            knife.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Mathf.Sign(transform.localScale.x) * knifeSpeed * (int)bias, 0f);
            Destroy(knife, lifeTime);
        }
    }
}
