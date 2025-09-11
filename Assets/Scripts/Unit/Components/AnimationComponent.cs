using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;

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
        animator.SetFloat("MoveSpeed", speed);
    }

    public void IsJump(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void IsFalling(bool isFalling)
    {
        animator.SetBool("IsFalling", isFalling);
    }

    public void IsLanding(bool isLanding, float landSpeed)
    {
        animator.SetBool("IsLanding", isLanding);
        animator.SetFloat("LandSpeed", landSpeed);
    }

}
