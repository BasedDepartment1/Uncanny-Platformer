using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    // Start is called before the first frame update
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        // var entityHealth = other.GetComponent<Health>();
        // entityHealth?.TakeDamage(knifeDamage);
        if (other.gameObject.CompareTag("Enemy")
        || other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(knifeDamage);
        }
        hitEntity = true;
        animator.SetBool("hitEntity", true);
        boxCollider.enabled = false;
    }

    public void SetDirection(float moveDirection)
    {
        direction = moveDirection;
        gameObject.SetActive(true);
        hitEntity = false;
        boxCollider.enabled = true;
        if (Mathf.Sign(transform.localScale.x) != direction)
        {
            FlipScale();
        }

        lifeTime = 0;
    }

    private void FlipScale()
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
