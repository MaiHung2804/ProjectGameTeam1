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

    private void OnEnable()
    {
        InputManager.Instance.OnJumpPressed += RequestJump;
        InputManager.Instance.OnMelleAttackPressed += RequestMeleeAttack;
        InputManager.Instance.OnRangedAttackPressed += RequestRangedAttack;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnJumpPressed -= RequestJump;
        InputManager.Instance.OnMelleAttackPressed -= RequestMeleeAttack;
        InputManager.Instance.OnRangedAttackPressed -= RequestRangedAttack;
    }


    void Start()
    {
        moveComponent = base.moveComponent as PlayerMoveComponent;
        attackComponent = base.attackComponent as PlayerAttackComponent;
        animationComponent = GetComponent<AnimationComponent>();

        // Set initial state
        currentState = PlayerState.Falling;
    }

  
    protected override void HandleActivities()
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
