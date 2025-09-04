using UnityEngine;

public class IdleState : IPlayerMoveState
{
    public void Enter(PlayerMoveComponent player)
    {
    }

    public void Update(PlayerMoveComponent player)
    {
        //if (player.HasMoveInput())
        //    player.ChangeState(new WalkState());
    }

    public void Exit(PlayerMoveComponent player)
    {
    }
}