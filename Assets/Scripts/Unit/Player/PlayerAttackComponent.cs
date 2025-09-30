using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
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
        
        UpdateStatusOfLastSkill();
        if (!canOutComponentState) return;

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


    private void UpdateStatusOfLastSkill()
    {
        switch (lastSkill)
        {
            case Skill.RangedAttack:
                canOutComponentState = animationComponent.IsRangedAttackingEnd;
                break;
            case Skill.MagicAttack:
                canOutComponentState = animationComponent.IsMagicAttackingEnd;
                break;
            case Skill.MeleeAttack:
            default: // Skill.None
                canOutComponentState = true;
                break;
        }
        if (canOutComponentState)
        {
            animationComponent.SkillAttack(lastSkill, false);
            Stop();
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
        //Debug.Log("PlayerAttackComponent Stop called." + currentSkill);
    }

}

