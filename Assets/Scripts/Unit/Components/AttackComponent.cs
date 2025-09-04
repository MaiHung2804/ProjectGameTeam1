using UnityEngine;

/// <summary>
/// Quan ly: sat thuong, thoi gian hoi chieu va thuc hien tan cong.
/// </summary>
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;

    private float lastAttackTime = -Mathf.Infinity;

    /// <summary>
    /// Sat thuong moi lan tan cong.
    /// </summary>
    public float AttackDamage => attackDamage;

    public float AttackRange => attackRange;

    /// <summary>
    /// Kiem tra co the tan cong khong
    /// </summary>
    public bool CanAttack => (Time.time >= lastAttackTime + attackCooldown);

    /// <summary>
    /// Thuc hien tan cong mot don vi muc tieu.
    /// </summary>
    public void Attack(UnitBase target)
    {
        if (target == null || !CanAttack) return;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= attackRange)
        {
            target.OnTakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }
}