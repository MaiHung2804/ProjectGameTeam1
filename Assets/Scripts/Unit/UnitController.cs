using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum UnitState
    {
        Idle,
        Moving,
        Attacking,
        Dead,
    }
    // Dieu phoi hanh vi chung (player, enemy, trap...).
    [RequireComponent(typeof(MoveComponent))]   // Dam bao rang co component. Khong co Unity tu tim
    [RequireComponent(typeof(AttackComponent))]
    public abstract class UnitController : MonoBehaviour
    {
        protected MoveComponent moveComponent;
        protected AttackComponent attackComponent;
        protected UnitBase unitBase;

        protected UnitState currentState = UnitState.Idle;

        protected virtual void Awake()
        {
            moveComponent = GetComponent<MoveComponent>();
            attackComponent = GetComponent<AttackComponent>();
            //unitBase = GetComponent<UnitBase>();  ?? 
        }

        protected virtual void Start()
        {
            currentState = UnitState.Idle;
        }

        protected virtual void Update()
        {
            if (unitBase != null && unitBase.IsDead)
            {
                ChangeState(UnitState.Dead);
                return;
            }

            switch (currentState)
            {
                case UnitState.Idle:
                    HandleIdle();
                    break;
                case UnitState.Moving:
                    HandleMoving();
                    break;
                case UnitState.Attacking:
                    HandleAttacking();
                    break;
                case UnitState.Dead:
                    HandleDead();
                    break;
            }
        }

    protected virtual void ChangeState(UnitState newState)
    {
       if (currentState == newState) return;
        currentState = newState;
        // Co the them logic khi thay doi trang thai o day
    }

    // Các cac ham se duoc override PlayerController/EnemyController
    protected abstract void HandleIdle();
    protected abstract void HandleMoving();
    protected abstract void HandleAttacking();
    protected abstract void HandleDead();
       
    }