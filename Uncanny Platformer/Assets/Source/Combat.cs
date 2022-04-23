using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float fireCooldown;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] throwingKnives;
    
    private float fireTimer = float.MaxValue;
    
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
