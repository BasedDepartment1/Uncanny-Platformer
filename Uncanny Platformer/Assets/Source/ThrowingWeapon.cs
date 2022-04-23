using System;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    [SerializeField] private float maxFlightSpeed;
    [SerializeField] private float knifeDamage;
    [SerializeField] private float maxLifeTime;

    private bool hitEntity;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator animator;
    
    private static readonly int HitEntity = Animator.StringToHash("hitEntity");

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (hitEntity)
        {
            return;
        }

        lifeTime += Time.deltaTime;
        var movementSpeed = maxFlightSpeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        
        if (lifeTime > maxLifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")
            || other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().ReduceHealthPoints(knifeDamage);
        }
        
        hitEntity = true;
        animator.SetBool(HitEntity, true);
        boxCollider.enabled = false;
    }

    public void SetDirection(float moveDirection)
    {
        direction = moveDirection;
        gameObject.SetActive(true);
        hitEntity = false;
        boxCollider.enabled = true;
        if (Math.Abs(Mathf.Sign(transform.localScale.x) - direction) > 1e-7f)
        {
            FlipScale();
        }

        lifeTime = 0;
    }

    private void FlipScale()
    {
        var transform1 = transform;
        var scale = transform1.localScale;
        scale.x *= -1;
        transform1.localScale = scale;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
