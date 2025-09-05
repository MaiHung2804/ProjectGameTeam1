using UnityEngine;

public class PlayerIdle : IPlayerMoveState
{
    public void Enter(PlayerMoveComponent player)
    {
        // animation idle
        player.MoveDirection(Vector3.zero);

    }

    public void Update(PlayerMoveComponent player)
    {
        if (player.HasMoveInput())
        {
            player.ChangeState(new PlayerWalkRun());
        }
    }

    public void Exit(PlayerMoveComponent player)
    {
    }
}