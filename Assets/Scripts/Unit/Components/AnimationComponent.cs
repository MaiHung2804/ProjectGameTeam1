using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;
    private bool isLandingEnd = false;

    protected virtual void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
            enabled = false; // Disable this component if Animator is not found
        }
    }

    public void MoveSpeed(float speed)
    {
        // MoveSpeed = 0 -> Idle
        // MoveSpeed = 2 -> Walk
        // MoveSpeed = 5 -> Run
        animator.SetFloat("MoveSpeed", speed);
    }

    public void Jumping(bool jumping)
    {
        animator.SetBool("Jumping", jumping);
    }

    public void Falling(bool falling)
    {
        animator.SetBool("Falling", falling);
    }

    public void Landing(bool landing, float landSpeed)
    {
        animator.SetBool("Landing", landing);
        animator.SetFloat("LandSpeed", landSpeed);
        if (landing)
        {
            isLandingEnd = false;
        }
    }

    public bool IsLandingEnd
    { get => isLandingEnd; }

    // Animation Event
    public void TurnOnLandingEnd()
    {
        isLandingEnd = true;
        // Debug.Log("Landing End");
    }

}
