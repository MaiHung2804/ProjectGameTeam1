using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCompentTest : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth => currentHealth;
    public void SetCurrentHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0f, maxHealth);
    }

    public void Initialize(float max, float current)
    {
        MaxHealth = max;
        SetCurrentHealth(current);
    }
    public void AllyData()
    {
        if (DataManager.Instance.player != null)
        {
            maxHealth = DataManager.Instance.player.maxHp;
            currentHealth = DataManager.Instance.player.currentHp;
        }
    }

    public void TakeDamage(float amount) => SetCurrentHealth(currentHealth - amount);
    public void Heal(float amount) => SetCurrentHealth(currentHealth + amount);

}
