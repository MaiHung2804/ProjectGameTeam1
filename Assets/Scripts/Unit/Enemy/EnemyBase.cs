using UnityEngine;

public class EnemyBase : UnitBase
{
    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 5f;     // Khoảng cách phát hiện Player
    [SerializeField] private float attackRange = 1.5f;      // Khoảng cách đánh
    [SerializeField] private float attackCooldown = 1.5f;   // Thời gian hồi chiêu

    private Transform targetPlayer;     // Player hiện tại
    private float lastAttackTime = 0f;  // Thời điểm lần đánh trước



    protected override void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (targetPlayer != null) return;

        PlayerBase player = FindObjectOfType<PlayerBase>();
        if (player != null)
        {
            targetPlayer = player.transform;
        }
    }

    protected void HandleMovement()
    {
        if (IsDead) return;

        if (targetPlayer == null)
        {
            FindPlayer();
            return;
        }

        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        // Nếu thấy Player trong phạm vi detectionRange mà chưa tới attackRange → đuổi theo
        if (distance > attackRange && distance <= detectionRange)
        {
            moveComponent?.MoveTo(targetPlayer.position);
        }
        else
        {
            moveComponent?.Stop();
        }
    }

    //protected void HandleMovement()
    //{         // Enemy movement logic can be implemented here if needed
    //}
   
    protected override void UpdateActions()
    { }
    //Enemy movement logic can be implemented here if needed
    protected void HandleAttack()
    {
        if (IsDead || targetPlayer == null) return;

        float distance = Vector3.Distance(transform.position, targetPlayer.position);

        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            UnitBase playerUnit = targetPlayer.GetComponent<UnitBase>();
            if (playerUnit != null)
            {
                attackComponent?.Attack(playerUnit);
            }
        }
    }
}