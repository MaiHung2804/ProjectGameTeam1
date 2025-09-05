using UnityEngine;

public interface IPlayerAttackState
{
    void Enter(PlayerAttackComponent player);
    void Update(PlayerAttackComponent player);
    void Exit(PlayerAttackComponent player);
}
