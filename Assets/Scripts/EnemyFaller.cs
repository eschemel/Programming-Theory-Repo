using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFaller : Enemy
{
    Rigidbody2D rigidbody2d;
    Animator animator;

    private float gravity = 0f;
    //float speed = 0.5f;
    //int direction = 1;
    float blinkTimer = 0f;

    Vector2 originPosition; // Position A

    Vector2 crushPosition; // Position B

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
        blinkTimer += Time.deltaTime;

        if (blinkTimer > 5f)
        {
            StartCoroutine("Blink");
        }
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Child trigger Enter");
            StartCoroutine("FallerDrop");
        } else if (rigidbody2d.position != originPosition)
        {
            StartCoroutine("FallerMove");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Child trigger Exit");
            StartCoroutine("FallerReset");
            StartCoroutine("FallerMove");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    IEnumerator FallerDrop()
    {
        yield return new WaitForSeconds(0.25f);
        gravity = 1f;
        rigidbody2d.gravityScale = gravity;

        rigidbody2d.AddForce(Vector2.down, ForceMode2D.Impulse);

        if (rigidbody2d.position == crushPosition)
        {
            yield break;
        }
    }

    IEnumerator FallerReset()
    {
        yield return new WaitForSeconds(3);
        gravity = 0f;
        rigidbody2d.gravityScale = gravity;

        // If the object has arrived, stop the coroutine
        if (rigidbody2d.position == originPosition)
        {
            yield break;
        }
    }

    IEnumerator FallerMove()
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime / 9.0f;
            rigidbody2d.position = Vector2.Lerp(rigidbody2d.position, originPosition, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (rigidbody2d.position == originPosition)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    IEnumerator Blink()
    {
        animator.SetBool("blink", true);
        blinkTimer = 0;
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("blink", false);
    }
}
