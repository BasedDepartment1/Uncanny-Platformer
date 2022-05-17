using UnityEngine;

namespace Source.PlayerLogic
{
    public class RangedCombat : MonoBehaviour
    {
        [SerializeField] private Player player;
    
        [SerializeField] private float fireCooldown;
        [SerializeField] private float lifeTime;
        [SerializeField] private float knifeSpeed;
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject throwingKnife;
        [SerializeField] private AnimationClip throwingAnimation;
    
        internal bool IsThrowStarted;
    
        private float fireTimer = float.MaxValue;

        private void Update()
        {
            fireTimer += Time.deltaTime;
            if (player.IsGrounded()
                && player.controls.IsRangedAttackPressed 
                && fireTimer > fireCooldown)
            {
                IsThrowStarted = true;
                Invoke(nameof(Fire), throwingAnimation.length * 0.6f);
                fireTimer = 0;
            }
        
            player.controls.IsRangedAttackPressed = false;
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
