using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : UnitBase
{
    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform targetPlayer;
    private float lastAttackTime = 0f;

    private void FindPlayer()
    {
        PlayerBase player = FindObjectOfType<PlayerBase>();
        if (player != null)
        {
            targetPlayer = player.transform;
        }
    }

    private void TryAttackPlayer()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;

        UnitBase playerUnit = targetPlayer.GetComponent<UnitBase>();
        if (playerUnit != null)
        {
            attackComponent.Attack(playerUnit);
        }
    }

    protected override void HandleMovement()
    {         // Enemy movement logic can be implemented here if needed
    }
    protected override void HandleAttack()
    {         // Enemy attack logic can be implemented here if needed
    }
}
