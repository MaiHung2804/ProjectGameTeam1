using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;
    private bool isLandingEnd = false;
    private bool isRangedAttackingEnd = false;
    private bool isMagicAttackingEnd = false;
    private bool isDyingEnd = false;

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
        //Debug.Log("Set MoveSpeed to " + speed +  " Animator " + animator);

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

    public void Die(bool dying)
    {
        animator.SetBool("Dying", dying);
    }

    public bool IsLandingEnd
    { get => isLandingEnd;
      set => isLandingEnd = value;
    }

    public bool IsRangedAttackingEnd
    {   get => isRangedAttackingEnd;
        set => isRangedAttackingEnd = value;
    }

    public bool IsMagicAttackingEnd
    {   get => isMagicAttackingEnd;
        set => isMagicAttackingEnd = value;
    }

    public bool IsDyingEnd
    {   get => isDyingEnd;
        set => isDyingEnd = value;
    }

    public void SkillAttack(Skill skill,bool isAttacking)
    {
        if (skill == Skill.None) return;
        if (skill == Skill.MeleeAttack) 
            animator.SetBool("MeleeAttack", isAttacking);

        if (skill == Skill.RangedAttack)
        {
            animator.SetBool("RangedAttack", isAttacking);
            if (isAttacking)
                isRangedAttackingEnd = false;
            else 
                isRangedAttackingEnd = true; // If not attacking, consider attack ended
        }
        if (skill == Skill.MagicAttack)
        {
            animator.SetBool("MagicAttack", isAttacking);
            if (isAttacking)
                isMagicAttackingEnd = false;
            else
                isMagicAttackingEnd = true; // If not attacking, consider attack ended
        }
    }

    // Animation Event
    public void TurnOnLandingEnd()
    {
        isLandingEnd = true;
        //Debug.Log("Landing End Animation Event");
    }
    public void TurnOnRangedAttackingEnd()
    {
        isRangedAttackingEnd = true;
        //Debug.Log("Ranged Attacking End Animation Event");
    }
    public void TurnOnMagicAttackingEnd()
    {
        isMagicAttackingEnd = true;
        //Debug.Log("Magic Attacking End Animation Event");
    }

    public void TurnOnDyingEnd()
    {
        isDyingEnd = true;
        //Debug.Log("Dying End Animation Event");
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
