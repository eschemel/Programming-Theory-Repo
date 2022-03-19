using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaller : Enemy
{
    private float gravity = 0f;
    private float blinkTimer = 0f;
    private int dropSpeed;
    private int dropDirection;

    private Vector2 originPosition;
    private Vector2 positionA;

    private Vector2 crushPosition;
    private Vector2 positionB;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d.gravityScale = gravity;

        originPosition = transform.position;

        crushPosition = new Vector2(rigidbody2d.position.x, -6.3f);
    }

    // Update is called once per frame
    private void Update()
    {
        //Blink animation
        blinkTimer += Time.deltaTime;
        if (blinkTimer > 5f)
        {
            StartCoroutine("Blink");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            //Debug.Log("Child trigger Enter");
            dropSpeed = 5;
            dropDirection = 1;
            positionA = originPosition;
            positionB = crushPosition;
            gravity = 1f;

            FallerMove(gravity, dropSpeed, dropDirection, positionA, positionB);
            StartCoroutine("ResetEnemy");
        }
    }

    private IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(5);
        dropSpeed = 15;
        dropDirection = -1;
        positionA = originPosition;
        positionB = crushPosition;
        gravity = 0f;

        FallerMove(gravity, dropSpeed, dropDirection, positionA, positionB);
    }
    
    private void FallerMove(float cGravity, int mSpeed, int mDirection, Vector2 positionA, Vector2 postionB)
    {
        rigidbody2d.gravityScale = cGravity;
        //Move
        float step = Time.deltaTime * mSpeed * mDirection;
        transform.position = Vector2.MoveTowards(positionA, postionB, step);
    }

    //Blink animation
    private IEnumerator Blink()
    {
        animator.SetBool("blink", true);
        blinkTimer = 0;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("blink", false);
    }

    public override void EnemyMove()
    {
        // override to remain stationary
    }
}
