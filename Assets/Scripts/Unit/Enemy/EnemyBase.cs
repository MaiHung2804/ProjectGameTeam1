using UnityEditor;
using UnityEngine;

public class EnemyBase : UnitBase
{

    private float maxSqrDetectRange;
    private float maxSqrAttackRange;
    private UnitBase target;

    public new EnemyData GetUnitData() => (EnemyData)unitData; // unitData nay la cua base bi ep kieu
    public void SetUnitData(EnemyData data) => unitData = data;

    public override void Init()
    {
        base.Init();
        // Lay Tam
        EnemyData unitData = GetUnitData();
        maxSqrDetectRange = unitData.DetectionRange * unitData.DetectionRange;
        maxSqrAttackRange = unitData.AttackRange * unitData.AttackRange;
    }   

    protected override void UpdateActions()
    {
        SelectState();
        ActionByState();
    }


    private void SelectState()
    {
        if (IsDead)
        {
            ChangeState(UnitState.Dead);
            return;
        }
        FindPlayer();

        float sqrDistance = (transform.position - target.transform.position).sqrMagnitude;

        if (sqrDistance < maxSqrAttackRange)
        {
            ChangeState(UnitState.Attack);
            return;
        }
        else
        {
            ChangeState(UnitState.Moving);
        }
        //// Nếu trong detectionRange thì di chuyển hoặc tấn công
        //if (sqrDistance <= attackComponent.AttackRange)
        //{
        //    moveComponent?.Stop();
        //    (attackComponent as EnemyAttackComponent)?.SetTarget(targetPlayer.GetComponent<UnitBase>());
        //    attackComponent?.HandleComponentActs();
        //}
        
        //else if (sqrDistance <= detectionRange)
        //{
        //    (moveComponent as EnemyMoveComponent)?.SetTarget(targetPlayer);
        //    moveComponent?.MoveTo(targetPlayer.position);
        //}
        //else
        //{
        //    moveComponent?.Stop();
        //    (attackComponent as EnemyAttackComponent)?.Stop();
        //}
    }

    private void ActionByState()
    {
        switch (CurrentState)
        {
            case UnitState.Moving:
            case UnitState.Idle:
                moveComponent.HandleComponentActs(target.transform.position);
                break;
            case UnitState.Attack:
                attackComponent.HandleComponentActs(target);
                break;
            case UnitState.Dead:
                break;
        }
    }

    private void ChangeState(UnitState newState)
    {
        if (CurrentState != newState)
        {
            if (CurrentState == UnitState.Moving)
            {
                moveComponent.Stop();
            }
            if (CurrentState == UnitState.Attack)
            {
                attackComponent.Stop();
            }
            CurrentState = newState;
            if (CurrentState == UnitState.Dead)
            {
                OnDeath();
            }
        }
    }

    private void FindPlayer()
    {
        if (target != null) return;
        target = PlayerManager.Instance.SelectedPlayerTarget();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        //// Co ve hoi thua Stop();
        //moveComponent.Stop();
        //attackComponent.Stop();
        animationComponent.Die(true);

    }
}
