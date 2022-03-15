using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody2d;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // virtual keyword allows overriding
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            if (animator != null)
            {
                animator.SetTrigger("hit");
            }
        }
    }
}