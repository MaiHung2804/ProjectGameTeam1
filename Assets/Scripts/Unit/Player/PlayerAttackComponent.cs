using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : AttackComponent
{
    [SerializeField] private AnimationComponent animationComponent;

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


    public override bool HasAttackInput()
    {
        return false; 
    }

    public override void HandleActivites()
    {

    }

    public override bool CanOutState()
    {
        return true;
    }
}

