using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCheckPoint : MonoBehaviour
{
    private BoxCollider2D m_Collider;
    private Animator animator;

    private Vector2 checkPointPosition;

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        m_Collider = GetComponent<BoxCollider2D>();
        checkPointPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.UpdateRespawnPosition(checkPointPosition);
            animator.SetTrigger("checkpointHit");
            m_Collider.enabled = !m_Collider.enabled;
        }
    }
}
