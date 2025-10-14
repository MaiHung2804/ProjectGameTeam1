using UnityEngine;

public class EnemyBase : UnitBase
{

    private float detectionRange = 8f;

    private Transform targetPlayer;

    public override void Init()
    {
        base.Init();
    }

    protected override void UpdateActions()
    {
        if (IsDead) return;
        FindPlayer();


        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        // Nếu trong detectionRange thì di chuyển hoặc tấn công
        if (distance <= attackComponent.AttackRange)
        {
            moveComponent?.Stop();
            (attackComponent as EnemyAttackComponent)?.SetTarget(targetPlayer.GetComponent<UnitBase>());
            attackComponent?.HandleComponentActs();
        }
        else if (distance <= detectionRange)
        {
            (moveComponent as EnemyMoveComponent)?.SetTarget(targetPlayer);
            moveComponent?.MoveTo(targetPlayer.position);
        }
        else
        {
            moveComponent?.Stop();
            (attackComponent as EnemyAttackComponent)?.Stop();
        }
    }




    private void FindPlayer()
    {
        if (targetPlayer != null) return;
        targetPlayer = PlayerManager.Instance.SelectedPlayerTarget();
    }
}
