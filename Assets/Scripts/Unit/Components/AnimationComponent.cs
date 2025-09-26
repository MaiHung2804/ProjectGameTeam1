using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;
    private bool isLandingEnd = false;


    public void InitComponent()
    {
        animator = GetComponent<Animator>();
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
        else
        {
            isLandingEnd = true; // If not landing, consider landing ended
            // Neu khong co cai nay thi co the lan di chuyen tiep theo, no se bao Landing End chua ket thuc
        }

    }

    public bool IsLandingEnd
    { get => isLandingEnd;
      set => isLandingEnd = value;
    }

    public void SkillAttack(Skill skill,bool isAttacking)
    {
        if (skill == Skill.None) return;
        if (skill == Skill.MeleeAttack) 
            animator.SetBool("MeleeAttack", isAttacking);

        if (skill == Skill.RangedAttack) 
            animator.SetBool("RangedAttack", isAttacking);
    }

    // Animation Event
    public void TurnOnLandingEnd()
    {
        isLandingEnd = true;
        Debug.Log("Landing End Animation Event");
    }
    public void Speed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }


}
