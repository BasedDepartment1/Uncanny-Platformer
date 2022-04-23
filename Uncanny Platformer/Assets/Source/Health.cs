using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;
    // private GameObject entity;

    private Animator animator;
    private bool isDead = false;
    public float CurrentHealth { get; private set; }

    [SerializeField] private float invisibilityDuration;
    [SerializeField] private int blinkCount;
    private bool isPlayer;

    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
        CurrentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
        // entity = gameObject;
    }

    public void TakeDamage(float damage)
    {
        // TODO make animations
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            // iFrameTimer = 0;
            if (isPlayer)
            {
                StartCoroutine(ActivateInvisibility());
            }
            // animator.SetTrigger("hurt");
        }
        else
        {
            if (isDead) return;
            // GetComponent<PlayerMovement>().enabled = false;
            // GetComponent<Image>().enabled = false;
            gameObject.SetActive(false);
            // animator.SetTrigger("die");
        }
    }

    private void Die()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }
    }

    private IEnumerator ActivateInvisibility()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        for (var i = 0; i < blinkCount; i++)
        {
            var blinkTime = invisibilityDuration / (blinkCount * 2);
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(blinkTime);
            sprite.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
