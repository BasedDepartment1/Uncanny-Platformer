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
            wasHurt = true;
        }
        else
        {
            isDead = true;
        }
    }

    internal void Die()
    {
        gameObject.SetActive(false);
    }

    internal void Revive()
    {
        CurrentHealth = startingHealth;
        isDead = false;
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
