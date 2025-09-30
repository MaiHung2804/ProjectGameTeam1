using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

/// <summary>      
/// Lop UnitBase la lop co so cho tat ca cac loai don vi trong game.
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    [Header("Unit Settings")]
    [SerializeField] protected Team teamID = Team.Player; // Player = 0
    public Team TeamID
    {
        get { return teamID; }
    }

    public enum UnitState
    {
        Idle, Moving, Attack, Dead
    }

    public UnitState CurrentState { get; protected set; } = UnitState.Idle;

    protected HealthComponent healthComponent;
    protected AttackComponent attackComponent;
    protected MoveComponent moveComponent;
    protected AnimationComponent animationComponent;

    public event Action<UnitBase> EventOnDeath;
   
    public bool IsDead => healthComponent != null && healthComponent.IsDead;
    public HealthComponent GetHealthComponent() => healthComponent;
    public AttackComponent GetAttackComponent() => attackComponent;
    public MoveComponent GetMoveComponent() => moveComponent;
    public AnimationComponent GetAnimationComponent() => animationComponent;

    

    protected virtual void Start()
    {
        InitComponent(); 
    }


    protected virtual void Update()
    {
        UpdateActions();
    }

    private void InitComponent()
    {
        healthComponent = GetComponent<HealthComponent>();
        attackComponent = GetComponent<AttackComponent>();
        moveComponent = GetComponent<MoveComponent>();
        animationComponent = GetComponent<AnimationComponent>();

        healthComponent.InitComponent();
        animationComponent.InitComponent();
        moveComponent.InitComponent();
        attackComponent.InitComponent();
    }


    protected virtual void UpdateActions() { }


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
        EventOnDeath?.Invoke(this);
        gameObject.SetActive(false); // Deactive thay vi destroy de co the tai su dung lai
    }






}