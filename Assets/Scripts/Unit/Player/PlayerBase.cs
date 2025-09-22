using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : UnitBase
{
    public enum PlayerState
    {
        Idle,
        Moving,
        Attacking,
        Dead
    }
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;
    PlayerMoveComponent moveComponent;
    PlayerAttackComponent attackComponent;





    void Start()
    {
        moveComponent = base.moveComponent as PlayerMoveComponent;
        attackComponent = base.attackComponent as PlayerAttackComponent;
    }

    //protected override void Update()
    //{
    //    base.Update();
    //}

    protected override void HandleMovement()
    {
        moveComponent.HandleActivites();
     

    }


    protected override void HandleAttack()
    {
    }

}
