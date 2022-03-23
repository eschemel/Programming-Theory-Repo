using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointEndGame : MonoBehaviour
{
    public ParticleSystem endEffect;
    private Animator animator;
    public AudioClip levelEnd;

    public CinemachineSwitcher cameraSwitch;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            } else
            {
                Debug.Log("Game Manager is null but Game Over achieved");
            }

            cameraSwitch.SwitchState();
            player.PlaySound(levelEnd, 1.0f);
            animator.SetTrigger("endHit");
            endEffect.gameObject.SetActive(true);
        }
    }
}
