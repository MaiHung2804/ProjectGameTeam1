using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PlayerBase : UnitBase
{
    public enum PlayerState
    {
        Idle,
        Moving,
        Jumping,
        Falling,
        Landing,
        MeleeAttacking,
        RangedAttacking,
        Dead
    }
    public PlayerState currentState { get; private set; } = PlayerState.Idle;
    PlayerMoveComponent moveComponent;
    PlayerAttackComponent attackComponent;

    private AnimationComponent animationComponent;
    private Vector2 moveInput;
    

    private bool isEngteringState = true;


    void Start()
    {
        moveComponent = base.moveComponent as PlayerMoveComponent;
        attackComponent = base.attackComponent as PlayerAttackComponent;
    }

  
    protected override void HandleInput()
    {
        if (IsFixedState(currentState))
            return;




    }

    protected override void HandleState()
    {

    }

    protected override void HandleAnimation()
    {

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
