using UnityEngine;

public class PlayerWalkRun : IPlayerMoveState
{


    public void Enter(PlayerMoveComponent player)
    {
        // animation run

    }

    public void Update(PlayerMoveComponent player)
    {
        if (!player.HasMoveInput())
        {
            player.ChangeState(new PlayerIdle());
            return;
        }
        Vector3 inputDirection = player.GetInputDirection();
        player.MoveDirection(inputDirection);
    }

    public void Exit(PlayerMoveComponent player)
    {
      
    }
}