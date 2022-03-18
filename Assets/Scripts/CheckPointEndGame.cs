using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEndGame : MonoBehaviour
{
    //Rigidbody2D rigidbody2d;
    Animator animator;

    private void Start()
    {
        //rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            GameManager.Instance.GameOver();
            animator.SetTrigger("endHit");
        }
    }
}
