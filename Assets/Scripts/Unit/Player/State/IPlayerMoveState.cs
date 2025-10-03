using UnityEngine;

public interface IPlayerMoveState
{
    void Enter(PlayerMoveComponent player);
    void Update(PlayerMoveComponent player);
    void Exit(PlayerMoveComponent player);
}