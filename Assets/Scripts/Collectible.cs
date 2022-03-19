using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    
    public GameObject powerUp;
    public GameObject usedBlock;

    private Vector2 originPosition;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originPosition = rigidbody2d.position;
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            animator.SetTrigger("hit");
            Instantiate(powerUp, new Vector2(originPosition.x, originPosition.y + 1.25f), Quaternion.identity);
            Instantiate(usedBlock, originPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
