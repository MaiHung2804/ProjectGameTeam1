using UnityEngine;

public interface IPlayerAttackState
{
    void Enter(AttackComponent player);
    void Update(AttackComponent player);
    void Exit(AttackComponent player);
}
