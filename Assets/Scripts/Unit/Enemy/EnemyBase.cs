using UnityEditor;
using UnityEngine;

public class EnemyBase : UnitBase
{
    private float maxSqrAttackRange;
    private UnitBase target;

    public new EnemyData GetUnitData() => (EnemyData)unitData; // unitData nay la cua base bi ep kieu
    public void SetUnitData(EnemyData data) => unitData = data;
    private const float UPDATE_TARGET_TIME = 1f;
    private float updateTargetTime = 0f;
    private float sqrDistanceTargetToEnemy;

    public override void Init()
    {
        base.Init();
        maxSqrAttackRange = unitData.AttackRange * unitData.AttackRange;
        FindPlayer();
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
        if (target.IsDead)
        {
            ChangeState(UnitState.Idle);
            return;
        }
        UpdateDistanceToTarget();
        if (sqrDistanceTargetToEnemy < maxSqrAttackRange)
        {
            ChangeState(UnitState.Attack);
            return;
        }
        else
        {
            ChangeState(UnitState.Moving);
        }


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
        Debug.Log("Enemy found player target: " + target);
    }

    private void UpdateDistanceToTarget()
    {
        if (updateTargetTime > 0)
        {
            updateTargetTime -= Time.deltaTime;
            return;
        }
        sqrDistanceTargetToEnemy = (target.transform.position - transform.position).sqrMagnitude;
        updateTargetTime = UPDATE_TARGET_TIME;
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
