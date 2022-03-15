using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaller : Enemy
{
    Rigidbody2D rigidbody2d;
    Animator animator;

    private float gravity = 0f;
    //float speed = 30f;
    //int direction = 1;
    float blinkTimer = 0f;
    int dropSpeed;
    int dropDirection;

    Vector2 originPosition; 
    Vector2 positionA;

    Vector2 crushPosition; 
    Vector2 positionB;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rigidbody2d.gravityScale = gravity;

        originPosition = transform.position;

        crushPosition = new Vector2(rigidbody2d.position.x, -6.3f);
    }

    // Update is called once per frame
    void Update()
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
            Debug.Log("Child trigger Enter");
            dropSpeed = 5;
            dropDirection = 1;
            positionA = originPosition;
            positionB = crushPosition;
            gravity = 1f;

            FallerMove(gravity, dropSpeed, dropDirection, positionA, positionB);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            Debug.Log("Child trigger Exit");
            dropSpeed = 2;
            dropDirection = 1;
            positionA = crushPosition;
            positionB = originPosition;
            gravity = 0f;

            FallerMove(gravity, dropSpeed, dropDirection, positionA, positionB);
        }
    }

    private void FallerMove(float cGravity, int mSpeed, int mDirection, Vector2 positionA, Vector2 postionB)
    {
        //Vector2 position = new Vector2(rigidbody2d.position.x, originPosition.y);

        //position.y = position.y + Time.deltaTime * mSpeed * mDirection;

        rigidbody2d.gravityScale = cGravity;
        //Move
        float step = Time.deltaTime * mSpeed * mDirection;
        transform.position = Vector2.MoveTowards(positionA, postionB, step);
    }

    //Blink animation
    IEnumerator Blink()
    {
        animator.SetBool("blink", true);
        blinkTimer = 0;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("blink", false);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        //print("Child hit with collider");
        animator.SetTrigger("hit");
    }
}
