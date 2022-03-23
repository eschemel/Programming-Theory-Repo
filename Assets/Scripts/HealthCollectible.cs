using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem sparkleEffect;
    public AudioClip pickupClip;

    private Vector2 particlePosition;

    private void Start()
    {
        particlePosition = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.health < player.maxHealth)
            {
                player.ChangeHealth(1);
                Destroy(gameObject);
                Instantiate(sparkleEffect, particlePosition, Quaternion.identity);
                player.PlaySound(pickupClip, 1.0f);
            }
        }
    }
}
