using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent
{
    private bool canOutComponentState = true;
    private Skill currentSkill = Skill.None;
    private Skill lastSkill = Skill.None;

    private Quaternion? targetRotation = null;
    private float rotationSpeed = 720f; // Xoay theo toc do /giay

    // TAM THOI LUU VU KHI MELEE O DAY
    public SwordCollider swordCollider;

    public override void InitComponent()
    {
        base.InitComponent();
        
        if (swordCollider == null)
        {
            swordCollider = GetComponentInChildren<SwordCollider>();
        }
        swordCollider.InitWeaponCollider(unitData.Damage);
        
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

            //TAM THOI LUU VU KHI MELEE O DAY
            if (currentSkill == Skill.MeleeAttack)
            {
                // Enable Sword Collider
                //  swordCollider.StartAttack();
                RotateToNearestEnemy();
            }

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

        // BO CAI NAY KEO DANH LIEN TUC
        //if (currentSkill == Skill.MeleeAttack)
        //{
        //    swordCollider.EndAttack();
        //}
        //Debug.Log("PlayerAttackComponent Stop called." + currentSkill);
    }

    private void RotateToNearestEnemy()
    {
        float searchRadius = PlayerManager.Instance.PlayerBase.GetUnitData().AttackRange;

        int enemyLayer = LayerMask.GetMask(LayerName.ENEMY);

        Collider[] hits = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);

        if (hits.Length == 0) return;

        Transform nearest = null;
        //Tim ke dich con song gan nhat
        float minDistance = float.MaxValue;
        foreach (var hit in hits)
        {
            UnitBase enemyUnit = hit.GetComponentInParent<UnitBase>();
            if (enemyUnit != null && !enemyUnit.IsDead)
            {
                float distance = Vector3.Distance(transform.position, enemyUnit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemyUnit.transform;
                }
            }
        }
        if (nearest != null)
        {
            Vector3 dir = (nearest.position - transform.position).normalized;
            dir.y = 0; // chỉ xoay theo trục ngang
            if (dir != Vector3.zero)
                targetRotation = Quaternion.LookRotation(dir);
        }

    }
    
    void Update()
    {
        if (targetRotation.HasValue)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(
             transform.rotation,
             targetRotation.Value,
             rotationSpeed * Time.deltaTime
         );
        if (Quaternion.Angle(transform.rotation, targetRotation.Value) < 1f)
        {
            transform.rotation = targetRotation.Value;
            targetRotation = null;
        }
    }

}

