using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditorInternal.VersionControl.ListControl;

public class PlayerBase : UnitBase
{
    private Vector2 moveInput = Vector2.zero;
    private bool jumpInput = false;
    private Skill attackInput = Skill.None;

    protected override void UpdateActions()
    {
        GetInput();
        SelectState();
        ActionByState();
    }

    private void GetInput()
    {
        moveInput = InputManager.Instance.GetMoveInput();
        jumpInput = InputManager.Instance.GetJumpInput();
        attackInput = InputManager.Instance.GetAttackInput();
    }

    private void SelectState()
    {
        if (IsDead)
        {
            ChangeState(UnitState.Dead);
            return;
        }
        else if (attackInput != Skill.None && moveComponent.CanOutComponentState())
        {   
            ChangeState(UnitState.Attack);
            return;
        }
        else if ( moveInput != Vector2.zero && attackComponent.CanOutComponentState())
        {
            ChangeState(UnitState.Moving);
            return;
        }
        else if (moveComponent.CanOutComponentState())
        {
            ChangeState(UnitState.Idle);
            return;
        }
    }

    private void ActionByState()
    {
        switch (CurrentState)
        {
            case UnitState.Moving:
            case UnitState.Idle:
                moveComponent.HandleComponentActs(moveInput, jumpInput);
                break;
            case UnitState.Attack:
                attackComponent.HandleComponentActs(attackInput);
                break;
            case UnitState.Dead:
                break;
        }
    }


    private void ChangeState(UnitState newState)
    {
        if (CurrentState != newState)
        {
            if ( CurrentState == UnitState.Moving)
            {
                moveComponent.Stop();
            }
            if (CurrentState == UnitState.Attack)
            {
                attackComponent.Stop();
            }
            CurrentState = newState;
        }
    }

    //private void ExitState(UnitState state)
    //{
    //    // Logic khi thoat trang thai
    //}

    //private void EnterState(UnitState state)
    //{
    //    // Logic khi vao trang thai
    //}


}
