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
    
    private bool isDead;
    private bool isPlayer;
    
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerMovement controls;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            controls = GetComponent<PlayerMovement>();
        }
        CurrentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ReduceHealthPoints(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            if (isPlayer)
            {
                animator.SetTrigger("hurt");
                StartCoroutine(ActivateInvisibility());
            }
        }
        else
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            animator.SetTrigger("death");
            controls.gameObject.SetActive(false);
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
