using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //public ParticleSystem sparkleEffect;
    //public AudioClip collectedClip;

    Vector2 particlePosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.health < player.maxHealth)
            {
                particlePosition = gameObject.transform.position;
                player.ChangeHealth(1);
                Destroy(gameObject);
                //Instantiate(sparkleEffect, particlePosition, Quaternion.identity);
                //player.PlaySound(collectedClip);
            }
        }
    }
}
