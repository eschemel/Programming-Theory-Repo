using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator animator;

    private bool playerFollowCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if(playerFollowCamera)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animator.Play("EndCamera");
        }
    }
}
