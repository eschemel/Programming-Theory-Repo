using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : Enemy
{
    SpriteRenderer spriteRenderer;

    float speed = 3.0f;
    float changeTime = 4.0f;
    float timer;
    int direction = 1;
    bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        PatrollerMove();
    }

    private void PatrollerMove()
    {
        Vector2 position = rigidbody2d.position;

        position.x = position.x + Time.deltaTime * speed * direction;
        Flip();

        rigidbody2d.MovePosition(position);
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        //replaced: void Flip()
        if (direction < 0.01f)
            spriteRenderer.flipX = false;
        else if (direction > -0.01f)
            spriteRenderer.flipX = true;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        //print("Child hit with collider");
    }
}
