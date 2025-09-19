using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform targetPlayer;
    private float lastAttackTime = 0f;

    private void Update()
    {
        if (IsDead) return;

        if (targetPlayer == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        if (distance <= attackRange)
        {
            TryAttackPlayer();
        }
        else if (distance <= detectionRange)
        {
            moveComponent?.MoveTo(targetPlayer.position);
        }
    }

    private void FindPlayer()
    {
        PlayerUnit player = FindObjectOfType<PlayerUnit>();
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
}
