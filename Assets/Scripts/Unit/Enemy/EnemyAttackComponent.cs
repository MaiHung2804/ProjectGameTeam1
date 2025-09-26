using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
    public override bool HasAttackInput()
    {
        // Enemy không cần input, chỉ tấn công khi trong range
        return false;
    }



    public override bool CanOutState()
    {
        return true; // Luôn có thể thoát khỏi state attack
    }

    public override void HandleActivities()
    {
        //    // Với enemy thì không cần xử lý phức tạp ở đây
        //    // Attack sẽ được gọi trong EnemyBase
    }
}
