using UnityEngine;
using System; 

public class HealthCompentTest : MonoBehaviour
{
    [Header("Health Info")]
    public int maxHealth;
    public int currentHealth;
    public PlayerData playerData;
    

    public event Action<int, int> OnHealthChanged; // Sự kiện để thông báo khi máu thay đổi

    

    public void SetHealth() 
    {
        playerData = DataManager.Instance.player;
        if (playerData != null)
        {
                maxHealth = playerData.maxHp;
                currentHealth = playerData.currentHp;
        }
        else
        {
                Debug.LogWarning(" No player data found in HealthCompentTest.");
        }
     } 

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);


    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
