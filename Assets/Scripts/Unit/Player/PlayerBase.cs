using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class PlayerBase : UnitBase
{
    public enum PlayerState
    {
        Idle,
        Moving,
        Attack,
        Dead
    }
    public PlayerState currentState { get; private set; } = PlayerState.Idle;
    PlayerMoveComponent moveComponent;
    PlayerAttackComponent attackComponent;

    private AnimationComponent animationComponent;
    private Vector2 moveInput;
    

    private bool isEngteringState = true;

    private void OnEnable()
    {
        PlayerInputManager.Instance.OnJumpPressed += RequestJump;
        PlayerInputManager.Instance.OnMelleAttackPressed += RequestMeleeAttack;
        PlayerInputManager.Instance.OnRangedAttackPressed += RequestRangedAttack;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.OnJumpPressed -= RequestJump;
        PlayerInputManager.Instance.OnMelleAttackPressed -= RequestMeleeAttack;
        PlayerInputManager.Instance.OnRangedAttackPressed -= RequestRangedAttack;
    }


    void Start()
    {
        moveComponent = base.moveComponent as PlayerMoveComponent;
        attackComponent = base.attackComponent as PlayerAttackComponent;
        animationComponent = GetComponent<AnimationComponent>();

        // Set initial state
        currentState = PlayerState.Idle;
    }

  
    protected override void HandleActivities()
    {
        if (currentState == PlayerState.Idle || currentState == PlayerState.Moving)
        {
            moveComponent.HandleActivites();
        }

        if (currentState == PlayerState.Attack)
        {
            attackComponent.HandleActivites();
        }
        HandleInput();

    }

    private void HandleInput()
    {
        PlayerState previousState = currentState;
        switch (currentState)
        {
            case PlayerState.Idle:
                if (moveInput.magnitude > 0.1f)
                {
                    currentState = PlayerState.Moving;
                }
                else if (attackComponent.IsAttacking)
                {
                    currentState = PlayerState.Attack;
                }
                break;
            case PlayerState.Moving:
                if (moveInput.magnitude <= 0.1f)
                {
                    currentState = PlayerState.Idle;
                }
                else if (attackComponent.IsAttacking)
                {
                    currentState = PlayerState.Attack;
                }
                break;
            case PlayerState.Attack:
                if (!attackComponent.IsAttacking)
                {
                    if (moveInput.magnitude > 0.1f)
                    {
                        currentState = PlayerState.Moving;
                    }
                    else
                    {
                        currentState = PlayerState.Idle;
                    }
                }
                break;
            case PlayerState.Dead:
                // Remain in Dead state
                break;
        }
        if (previousState != currentState)
        {
            isEngteringState = true;
            OnExitState(previousState);
            OnEnterState(currentState);
        }
        else
        {
            isEngteringState = false;
        }
        animationComponent.UpdateAnimation(currentState, isEngteringState);
    }





    private bool IsFixedState(PlayerState currentState)
    {
        if (currentState == PlayerState.Jumping 
            || currentState == PlayerState.Falling || 
            currentState == PlayerState.Landing 
            || currentState == PlayerState.Dead)
            return true;
        return false;
    }
}
