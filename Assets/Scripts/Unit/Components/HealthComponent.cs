using UnityEngine;

/// <summary>
/// Quan ly mau, nhan sat thuong va kiem tra trang thai song/chet cua unit.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;    // Se lay trong UnitData sau
    [SerializeField] private float currentHealth;

    public bool IsDead
    {
        get { return currentHealth <= 0f; }
    }
    public float MaxHealth => maxHealth;

    public float CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <param name="damage"> nhan sat thuong damage va giam.</param>
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    //  Hoi mau cho don vi.
    public void Cure(float amount)
    {
        if (IsDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
}