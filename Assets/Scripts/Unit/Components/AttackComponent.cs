using System;
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

    // Phat su kien khi tan cong thanh cong
    public event Action <UnitBase> EventOnAttackSuccess;

    public float AttackDamage => attackDamage;

    public float AttackRange => attackRange;

    // Thoi gian hoi chieu giua cac dot tan cong
    public float AttackCooldown => attackCooldown;

    // Kiem tra xem don vi co the tan cong hay khong
    public bool CanAttack => (Time.time >= lastAttackTime + attackCooldown);

    ///<summary>
    ///Kiem tra muc tieu hop le de tan cong
    ///CAI NAY CO THE SUA DOI THEM, VD: KIEM TRA CO DUONG DI, CO NHIN THAY KHONG
    ///</summary>
    public bool IsValidTarget(UnitBase target)
    {
        if (target == null) return false;
        if (target.IsDead) return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= attackRange;
    }


    // Thuc hien tan cong muc tieu neu hop le
    public void Attack(UnitBase target)
    {
        if (!CanAttack) return;
        if (!IsValidTarget(target)) return;
        
        target.OnTakeDamage(attackDamage);
        lastAttackTime = Time.time;

        // Phat su kien tan cong thanh cong
        EventOnAttackSuccess?.Invoke(target);

        // HIEU UNG TAN CONG O DAY (NEU CAN)
    }

    /// <summary>
    /// Reset thoi gian hoi chieu, cho phep tan cong ngay lap tuc
    /// </summary> 
    public void ResetCooldown()
    {
        lastAttackTime = -Mathf.Infinity;
    }
}