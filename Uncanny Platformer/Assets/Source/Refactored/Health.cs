using System.Collections;
using UnityEngine;

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
    
    internal bool isDead;
    internal bool wasHurt;

    // private Animator animator;
    private SpriteRenderer sprite;
    // private PlayerMovement controls;
    
    void Awake()
    {
        // animator = GetComponent<Animator>();
        // if (gameObject.CompareTag("Player"))
        // {
        //     isPlayer = true;
        //     controls = GetComponent<PlayerMovement>();
        // }
        CurrentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ReduceHealthPoints(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            StartCoroutine(ActivateInvisibility());
            wasHurt = true;
            // animator.SetTrigger("hurt");
        }
        else
        {
            isDead = true;
            // animator.SetBool("isDying", true);
            // animator.SetTrigger("death");
        }
    }

    private void Die()
    {
        // Debug.Log("zxc");
        isDead = true;
        gameObject.SetActive(false);
    }

    internal void Revive()
    {
        CurrentHealth = startingHealth;
        isDead = false;
    }

    private IEnumerator ActivateInvisibility()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        for (var i = 0; i < blinkCount; i++)
        {
            var blinkTime = invisibilityDuration / (blinkCount * 2);
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(blinkTime);
            sprite.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }
}
