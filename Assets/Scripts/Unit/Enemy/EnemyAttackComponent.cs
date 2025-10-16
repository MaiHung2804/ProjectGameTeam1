using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
    private UnitBase target;
    private bool isAttacking = false;
    private float maxSqrAttackRange;
    private int damagePerTick;
    private float attackTickTimer = 0;
    private const float ATTACK_TOTAL_TIME = 5f;
    private const int ATTACK_TICK_COUNT = 5;
    private const float ATTACK_TICK_INTERVAL = ATTACK_TOTAL_TIME / ATTACK_TICK_COUNT;

    public override void InitComponent()
    {
        base.InitComponent();
        maxSqrAttackRange = attackRange * attackRange;
        damagePerTick =  attackDamage / ATTACK_TICK_COUNT;
    }

    public override void HandleComponentActs(UnitBase inputTarget)
    {
        target = inputTarget;
        if (inputTarget == null) return;
        Attack();
       
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animationComponent.SkillAttack(Skill.MeleeAttack, true);
        }
        if (attackTickTimer < ATTACK_TICK_INTERVAL)
        {
            attackTickTimer += Time.deltaTime;
            return;
        }
        else 
        {
            attackTickTimer = 0;
            target.OnTakeDamage(damagePerTick);
        }



        // LAM TAM THOI, CHUA DAT COLLIDER O BAN TAY NHU MODUN TRUOC

        // Here you would typically trigger damage application logic
    }

    public override bool CanOutComponentState()
    {
        return true;
    }

    public override void Stop()
    {
        if (!isAttacking) return;
        isAttacking = false;
        animationComponent.SkillAttack(Skill.MeleeAttack, false);
    }

   



}


