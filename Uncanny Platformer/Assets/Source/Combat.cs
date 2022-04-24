using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float fireCooldown;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] throwingKnives;
    
    private Animator animator;
    private float fireTimer = float.MaxValue;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && fireTimer > fireCooldown)
        {
            Fire();
        }

        fireTimer += Time.deltaTime;
    }

    private void Fire()
    {
        fireTimer = 0;
        var knifeIndex = FindKnifeIndex();
        throwingKnives[knifeIndex].transform.position = firePosition.position;
        throwingKnives[knifeIndex].GetComponent<ThrowingWeapon>()
            .SetDirection(Mathf.Sign(transform.localScale.x));
        animator.SetTrigger("throw");
    }

    private void EndThrow()
    {
        
    }
    
    private int FindKnifeIndex()
    {
        for (var i = 0; i < throwingKnives.Length; i++)
        {
            if (!throwingKnives[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }
}
