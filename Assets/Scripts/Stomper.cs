using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomper : MonoBehaviour
{
    private Rigidbody2D playerRB;
    public AudioClip hitClip;
    private AudioSource audioSource;

    private float bounceForce = 15f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.parent.GetComponent<Rigidbody2D>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Hurtbox")
        {
            Destroy(other.gameObject);
            playerRB.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(hitClip, 1.0f);
        }
    }
}
