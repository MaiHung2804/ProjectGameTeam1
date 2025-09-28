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
        if (!isLastSkillEnd())
        {
            return;
        }

        currentSkill = skill;
        switch (currentSkill)
        {
            case Skill.None:
                canOutComponentState = true;
                break;
            default:
                DoSkill();
                break;
        }
    }

    private void DoSkill()
    {
        if (currentSkill != lastSkill)
        {
            canOutComponentState = false;
            animationComponent.SkillAttack(currentSkill, true);
            lastSkill = currentSkill;
        }
    }


    private bool isLastSkillEnd()
    {
        switch (lastSkill)
        {
            case Skill.MeleeAttack:
                canOutComponentState = true;
                return true; 
            case Skill.RangedAttack:
                canOutComponentState = false;
                return animationComponent.IsRangedAttackingEnd;
            case Skill.MagicAttack:
                canOutComponentState = false;
                return animationComponent.IsMagicAttackingEnd;
            default: // Skill.None
                canOutComponentState = true;
                return true;
        }
    }

    public override bool CanOutComponentState()
    {
        return canOutComponentState;
    }

    public override void Stop()
    {
        
        canOutComponentState = true;
        animationComponent.SkillAttack(currentSkill, false);
        currentSkill = Skill.None;
        lastSkill = Skill.None;
        Debug.Log("PlayerAttackComponent Stop called." + currentSkill);
    }

}

