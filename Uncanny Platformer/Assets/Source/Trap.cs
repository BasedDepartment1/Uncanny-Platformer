using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapType trapType;
    [Header("For trap")]
    [SerializeField] private float trapDamage;

    [Header("For jumper")] 
    [SerializeField] private float jumpForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        var health = other.GetComponent<Health>();
        var body = other.GetComponent<Rigidbody2D>();
        switch (trapType)
        {
            case TrapType.Trap:
                health.ReduceHealthPoints(trapDamage);
                break;
            case TrapType.Jumper:
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                break;
            case TrapType.Deathbox:
                health.ReduceHealthPoints(health.CurrentHealth * 10);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
