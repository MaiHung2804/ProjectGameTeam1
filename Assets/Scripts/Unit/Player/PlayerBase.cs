using UnityEngine;


public class PlayerBase : UnitBase
{
    private Vector2 moveInput = Vector2.zero;
    private bool jumpInput = false;
    private Skill attackInput = Skill.None;
    private float minMeleeAttackTime = 1f;
    private float lastMeleeAttackTime = -Mathf.Infinity;

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

        // TEST TAKE DAMAGE
        if (Input.GetKeyDown(KeyCode.T))
        {
            healthComponent.TakeDamage(20);
        }

        if (attackInput == Skill.MeleeAttack)
        {
            lastMeleeAttackTime = Time.time;
        }
        if (attackInput == Skill.None && Time.time - lastMeleeAttackTime < minMeleeAttackTime)
        {
            attackInput = Skill.MeleeAttack;
        }

    }

    private void SelectState()
    {
        if (IsDead)
        {
            ChangeState(UnitState.Dead);
            return;
        }
        if (!attackComponent.CanOutComponentState())
        {
            ChangeState(UnitState.Attack);
            return;
        }
        else if (attackInput != Skill.None && moveComponent.CanOutComponentState())
        {
            ChangeState(UnitState.Attack);
            return;
        }
        else if (moveInput != Vector2.zero) //&& attackComponent.CanOutComponentState())
        {
            ChangeState(UnitState.Moving);
            return;
        }
        else if (moveComponent.CanOutComponentState())
        {
            //Debug.Log(" AttackComponent.CanOutComponentState()" + attackComponent.CanOutComponentState());
            ChangeState(UnitState.Idle);
            return;
        }
    }
    // // ActionByState: Thuc hien hanh dong theo trang thai hien tai

    private void ActionByState()
    {
        //Debug.Log("Current State: " + CurrentState);
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

    protected override void OnDeath()
    {
        base.OnDeath();
        moveComponent.Stop();
        attackComponent.Stop();
        animationComponent.Die(true);

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
