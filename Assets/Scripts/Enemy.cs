using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody2d;
    protected SpriteRenderer spriteRenderer;

    protected float speed = 3.0f;
    protected float changeTime = 3.0f;
    protected float timer;
    protected int direction = 1;
    protected bool facingRight = false;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

    public virtual void EnemyMove()
    {
        Vector2 position = rigidbody2d.position;

        position.x = position.x + Time.deltaTime * speed * direction;
        Flip();

        rigidbody2d.MovePosition(position);
    }

    public virtual void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        //replaced: void Flip()
        if (direction < 0.01f)
            spriteRenderer.flipX = false;
        else if (direction > -0.01f)
            spriteRenderer.flipX = true;
    }
}