using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackArea;

    private Controls controls;
    private Animator animator;
    private AttackHitbox hitbox;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitbox = attackArea.GetComponent<AttackHitbox>();
        controls = GetComponent<Controls>();
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    private void ActivateHitbox()
    {
        hitbox.SetHitboxStatus(true);
        controls.enabled = false;
    }

    private void DeactivateHitbox()
    {
        hitbox.SetHitboxStatus(false);
        controls.enabled = true;
    }
}
