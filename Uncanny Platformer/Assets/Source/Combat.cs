using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float fireCooldown;
    private PlayerMovement movement;
    private float fireTimer = float.MaxValue;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject[] throwingKnives;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
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
    }

    // private Rigidbody2D body;
    // [SerializeField] private float hitJumpHeight = 5f;
    //
    // void Start()
    // {
    //     body = GetComponent<Rigidbody2D>();
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         // other.gameObject.GetComponent<Health>().TakeDamage(200);
    //         body.velocity = new Vector2(-body.velocity.x, hitJumpHeight);
    //         gameObject.GetComponent<Health>().TakeDamage(66);
    //     }
    // }
    //
    // // Start is called before the first frame update
    //
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

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
