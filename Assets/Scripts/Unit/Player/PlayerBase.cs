using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditorInternal.VersionControl.ListControl;

public class PlayerBase : UnitBase
{
    
    //private PlayerMoveComponent moveComponent;
    //private PlayerAttackComponent attackComponent;

    private Vector2 moveInput = Vector2.zero;
    private bool isJump = false;
    private bool isAttack = false;   // Se sua sau khi co Enum Skill


    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    void Start()
    {
        //moveComponent = base.moveComponent as PlayerMoveComponent;
        //attackComponent = base.attackComponent as PlayerAttackComponent;
    }

  
    protected override void HandleActivities()
    {
        GetInput();
        SelectOverallState();
        HandleStateActivities();
    }

    private void GetInput()
    {
        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        isJump = InputManager.Instance.IsJumpPressed();


        //bool attackInput = InputManager.Instance.GetAttackInput();
    }

    private void SelectOverallState()
    {
        if (IsDead)
        {
            ChangeState(UnitState.Dead);
            return;
        }
        else if (isAttack && moveComponent.CanOutSate())
        {   
            ChangeState(UnitState.Attack);
            return;
        }
        else if ( moveInput != Vector2.zero && attackComponent.CanOutState())
        {
            ChangeState(UnitState.Moving);
            return;
        }
        else if (moveComponent.CanOutSate())
        {
            ChangeState(UnitState.Idle);
            return;
        }
    }

    private void HandleStateActivities()
    {
        switch (currentState)
        {
            case UnitState.Moving:
            case UnitState.Idle:
                moveComponent.HandleActivities(moveInput, isJump);
                break;
            case UnitState.Attack:
                attackComponent.HandleActivities();
                break;
            case UnitState.Dead:
                break;
        }
    }


    private void ChangeState(UnitState newState)
    {
        if (currentState != newState)
        {
            // ExitState(currentState);
            currentState = newState;
            // EnterState(newState);
        }
    }

    private void ExitState(UnitState state)
    {
        // Logic khi thoat trang thai
    }

    private void EnterState(UnitState state)
    {
        // Logic khi vao trang thai
    }


}
