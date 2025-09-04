using UnityEngine;

public class WalkState : IPlayerMoveState
{
    public void Enter(PlayerMoveComponent player)
    {
        
    }

    public void Update(PlayerMoveComponent player)
    {
        //player.MoveDirection(player.GetInputDirection());

        // Kiem tra chuyen trang thai
        //if (!player.HasMoveInput())
        //    player.ChangeState(new IdleState());
        // Them dieu kien chuyen trang thai khac o day neu can
    }

    public void Exit(PlayerMoveComponent player)
    {
      
    }
}