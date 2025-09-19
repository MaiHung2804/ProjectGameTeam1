using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController
{
    private PlayerMoveComponent playerMove;
    private PlayerAttackComponent playerAttack;


    private float currentSpeed = 0f;
    private Vector3 currentDir = Vector3.zero;
    private Vector3 lastDir = Vector3.zero;

    // Cac bien tam thoi
    [SerializeField] private Camera mainCamera;


    protected override void Awake()
    {
        base.Awake();
        playerMove = base.moveComponent as PlayerMoveComponent; // coi lop cha MoveComponent nhu PlayerMoveComponent
        playerAttack = base.attackComponent as PlayerAttackComponent;
    }

    protected override void HandleIdle()
    {
        if (playerMove.HasMovementInput())
        {
            ChangeState(UnitState.Moving);
        }
        if (playerAttack.HasAttackInput())
        {
            ChangeState(UnitState.Attacking);
        }
        playerMove.HandleActivites();
    }

    protected override void HandleMoving()
    {
        if ( !playerMove.CanOutSate() )
        {
            return;
        }
        
        if (!playerMove.HasMovementInput())
        {
            ChangeState(UnitState.Idle);
            return;
        }
        if (playerAttack.HasAttackInput())
        {
            ChangeState(UnitState.Attacking);
            return;
        }
        
        playerMove.HandleActivites();
    }

    protected override void HandleAttacking()
    {
        if (!playerAttack.CanOutState())
        {
            return;
        }
        if (!playerAttack.HasAttackInput())
        {
            ChangeState(UnitState.Idle);
            return;
        }
        playerAttack.HandleActivites();
    }

    protected override void HandleDead()
    {
    }



}
