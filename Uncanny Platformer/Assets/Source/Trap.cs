using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float trapDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        var health = other.GetComponent<Health>();

        health.ReduceHealthPoints(trapDamage);
        
    }
}
