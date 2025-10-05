using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
    private UnitBase target;

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


//using UnityEngine;

//public class EnemyAttackComponent : AttackComponent
//{
//    //public override bool HasAttackInput()
//    //{
//    //    // Enemy không cần input, chỉ tấn công khi trong range
//    //    return false;
//    //}



//    //public override bool CanOutState()
//    //{
//    //    return true; // Luôn có thể thoát khỏi state attack
//    //}

//    //public override void HandleActivities()
//    //{
//    //    //    // Với enemy thì không cần xử lý phức tạp ở đây
//    //    //    // Attack sẽ được gọi trong EnemyBase
//    //}
//    public override bool CanOutComponentState()
//    {
//        return false; //todo
//    }

//    public override void Stop()
//    {
//        //todo
//    }
//}
