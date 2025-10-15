using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
    private UnitBase target;
    private EnemyData enemyData;

    public void SetTarget(UnitBase newTarget)
    {
        target = newTarget;
    }

    public override void HandleComponentActs()
    {
        if (target == null) return;

        if (IsValidTarget(target) && CanAttack)
        {
            Attack(target);
        }
    }

    public override bool CanOutComponentState()
    {
        // Thoát khỏi state tấn công nếu không còn target hợp lệ
        return target == null || target.IsDead || !IsValidTarget(target);
    }

    public override void Stop()
    {
        target = null;
    }
}


