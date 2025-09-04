using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>      
/// Lop UnitBase la lop co so cho tat ca cac loai don vi trong game.
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    [Header("Unit Settings")]
    [SerializeField] protected int teamID = 0;

    protected HealthComponent healthComponent;
    protected AttackComponent attackComponent;
    protected MoveComponent moveComponent;

    public int TeamID => teamID;

    public bool IsDead
    {
        get 
        { return healthComponent != null && healthComponent.IsDead;
        }
    }


    public HealthComponent GetHealthComponent() => healthComponent;

    public AttackComponent GetAttackComponent() => attackComponent;

    public MoveComponent GetMoveComponent() => moveComponent;

     /// <param name="damage">  ghi chu luong sat thuong </param>
    public virtual void OnTakeDamage(float damage)
    {
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
            if (healthComponent.IsDead)
            {
                OnDeath();
            }
        }
    }

    // Logic khi unit chet
    protected virtual void OnDeath()
    {
        gameObject.SetActive(false);
    }


    protected virtual void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        attackComponent = GetComponent<AttackComponent>();
        moveComponent = GetComponent<MoveComponent>();
    }
}