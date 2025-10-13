using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat 
{
    public UnitConfig config;
    public int level; // Cấp độ
    public int currentExperience; // Kinh nghiệm hiện tại
    public int maxExperience; // Kinh nghiệm tối đa để lên cấp
    public int currentHealth; // Máu hiện tại
    public int maxHealth; // Máu tối đa
    public int attackBase; // Sát thương
    public int attackSpeed; // Tốc độ đánh
    public int defenseBase; // Phòng thủ
    public float moveSpeed; // Tốc độ di chuyển
    public int attackRange; // Tầm đánh
    
    public UnitStat(UnitConfig config)
    {
        this.config = config;
        this.level = 1; // Mặc định cấp độ là 1
        this.currentExperience = 0; // Mặc định kinh nghiệm hiện tại là 0
        this.maxExperience = 100; // Mặc định kinh nghiệm tối đa là 100
        this.maxHealth = 100; // Mặc định máu tối đa là 100
        this.currentHealth = maxHealth; // Khởi tạo máu hiện tại bằng máu tối đa
        this.attackBase = 10; // Mặc định sát thương cơ bản là 10
        this.attackSpeed = 1; // Mặc định tốc độ đánh là 1
        this.defenseBase = 5; // Mặc định phòng thủ cơ bản là 5
        this.moveSpeed = 5; // Mặc định tốc độ di chuyển là 5
        //this.attackRange = config.type == EnemyType.Melee ? 1 : 5; // Tầm đánh dựa trên loại kẻ địch
    }

    public void GainExperience(int amount) // Nhận kinh nghiệm
    {
        currentExperience += amount;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }
    public void LevelUp() // Lên cấp
    {
        level++;
        DamageUpg(level); // Nâng cấp sát thương
        HealthUp(level); // Nâng cấp máu
        DefenseUpg(level); // Nâng cấp phòng thủ
    }
    public void TakeDamage(int damage) // Nhận sát thương
    {
        int damageTaken = damage - defenseBase;
        if (damageTaken < 0)
        {
            damageTaken = 0;
        }
        currentHealth -= damageTaken;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public void Die() // Chết
    {
        
        Debug.Log(config.Name + " has died.");
    } 
    public void Heal(int amount) // Hồi máu
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void DamageUpg(int upgradeAmount) // Nâng cấp sát thương
    {
        upgradeAmount = upgradeAmount * 5; // Mỗi lần nâng cấp tăng thêm 5 sát thương
        attackBase += upgradeAmount;
    }
    public void HealthUp(int upgradeAmount) // Nâng cấp máu
    {
        upgradeAmount = upgradeAmount * 10; // Mỗi lần nâng cấp tăng thêm 10 máu
        maxHealth += upgradeAmount;
        currentHealth = maxHealth; // Hồi máu đầy khi nâng cấp
        Debug.Log("Health upgraded by " + upgradeAmount + ". New HP: " + maxHealth);
    }
    public void DefenseUpg(int upgradeAmount) // Nâng cấp phòng thủ
    {
        upgradeAmount = upgradeAmount * 2; // Mỗi lần nâng cấp tăng thêm 2 phòng thủ
        defenseBase += upgradeAmount;
    }


}
