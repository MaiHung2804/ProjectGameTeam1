using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent
{
    private AnimationComponent animationComponent;
    private bool canOutComponentState = true;
    private Skill currentSkill = Skill.None;
    private Skill lastSkill = Skill.None;


    //[SerializeField] private GameObject rangedWeaponPrefab;

    public override void InitComponent()
    {
        UnitBase unit = GetComponent<UnitBase>();
        animationComponent = unit.GetAnimationComponent();
    }


    public override void HandleComponentActs(Skill skill)
    {
        currentSkill = skill;
        switch (currentSkill)
        {
            case Skill.None:
                canOutComponentState = true;
                break;
            case Skill.MeleeAttack:
                SkillMeleeAttack();
                break;
            case Skill.RangedAttack:
                SkillRangedAttack();
                break;
            default:
                canOutComponentState = true;
                break;
        }
    }

    private void SkillMeleeAttack()
    {
        if (currentSkill != lastSkill)
        {
            canOutComponentState = false;
            animationComponent.SkillAttack(currentSkill, true);
            lastSkill = currentSkill;
        }

    }

    private void SkillRangedAttack()
    { 
        Debug.Log("Ranged Attack Skill activated.");
    }


    // Goi ham nay tu PlayerController de tan cong mot muc tieu
    public void AttackTarget(UnitBase target, int skillIndex = 0)
    {
        if (!CanAttack) return;
        if (!IsValidTarget(target)) return;

        // Goi animation tan cong (co the truyen skillIndex neu co nhieu chieu)
        if (animationComponent != null)
        {
            // animationComponent.PlayAttackAnimation(skillIndex);
        }

        // Gay sat thuong cho muc tieu
        base.Attack(target);
    }


    public override bool CanOutComponentState()
    {
        return canOutComponentState;
    }

    public override void Stop()
    {
        Debug.Log("PlayerAttackComponent Stop called.");
        canOutComponentState = true;
        animationComponent.SkillAttack(currentSkill, false);
    }

}

