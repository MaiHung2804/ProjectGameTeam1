using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData 
{
    public string userName;
    public int highScore;
    public int userDamage;
    public int userDefense;
    public int maxHp;
    public int currentHp;
    public int maxMana;
    public int currentMana;
    public int userLevel;
    public int currentLevel;
    public int userGold;
    public int currentExperience;
    public int experienceToNextLevel; 
    public string currentScene;
    public Vector3 userPosition;

    // Constructor để khởi tạo dữ liệu người chơi (thiếu gì nhắn em để thêm)
    public PlayerData(string name, int score, int dmg, int def, int hp, int mp, int lv, int gold, Vector3 position, int exp, string scene)
    {
        this.userName = name;
        this.highScore = score;
        this.userDamage = dmg;
        this.userDefense = def;
        this.maxHp = hp;
        this.currentHp = hp;
        this.maxMana = mp;
        this.currentMana = mp;
        this.userLevel = lv;
        this.userGold = gold;
        this.userPosition = position;
        this.currentExperience = exp;
        this.experienceToNextLevel = lv * 100;
        this.currentLevel = lv;
        this.currentScene = scene;

    }

    public Vector3 GetPosition() // Lấy vị trí
    {
        return userPosition;
    }
    public void SetPosition(Vector3 position) // Đặt vị trí
    {
        userPosition = position;
    }
    public void LevelUp() // Lên cấp
    {
        userLevel++;
        currentLevel = userLevel;
        DamageUpg(userLevel); // Nâng cấp sát thương
        HealthUp(userLevel); // Nâng cấp máu
        DefenseUpg(userLevel); // Nâng cấp phòng thủ
        Debug.Log("Level Up! New Level: " + userLevel + ", New HP: " + maxHp);
    }
    public void AddGold(int amount) // Thêm vàng
    {
        userGold += amount;
        Debug.Log("Added " + amount + " gold. Total Gold: " + userGold);
    }
    public void TakeDamage(int damage) // Nhận sát thương
    {
        int damageTaken = damage - userDefense;
        if (damageTaken < 0)
        {
            damageTaken = 0;
        }
        currentHp -= damageTaken;
        Debug.Log("Took " + damageTaken + " damage. Current HP: " + currentHp);
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }
    public void Die() // Chết
    {
        
        Debug.Log(userName + " has died.");
             
    }
    public void TakeExperience(int experience) // Nhận kinh nghiệm
    {
        currentExperience += experience;
        Debug.Log("Gained " + experience + " XP. Total XP: " + currentExperience);
        // Giả sử mỗi cấp cần 100 XP để lên cấp tiếp theo
        experienceToNextLevel = currentLevel * 100;
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
            
            currentExperience -= experienceToNextLevel; // Giữ lại XP thừa
        }
    }
    public void DamageUpg(int upgradeAmount) // Nâng cấp sát thương
    {
        upgradeAmount = upgradeAmount * 5; // Mỗi lần nâng cấp tăng thêm 5 sát thương
        userDamage += upgradeAmount;
    }
    public void HealthUp(int upgradeAmount) // Nâng cấp máu
    {
        upgradeAmount = upgradeAmount * 10; // Mỗi lần nâng cấp tăng thêm 10 máu
        maxHp += upgradeAmount;
        currentHp = maxHp; // Hồi máu đầy khi nâng cấp
        Debug.Log("Health upgraded by " + upgradeAmount + ". New HP: " + maxHp);
    }
    public void ManaUp(int upgradeAmount) // Nâng cấp mana
    {
        upgradeAmount = upgradeAmount * 5; // Mỗi lần nâng cấp tăng thêm 5 mana
        maxMana += upgradeAmount;
        currentMana = maxMana; // Hồi mana đầy khi nâng cấp
        Debug.Log("Mana upgraded by " + upgradeAmount + ". New Mana: " + maxMana);
    }
    public void DefenseUpg(int upgradeAmount) // Nâng cấp phòng thủ
    {
        upgradeAmount = upgradeAmount * 2; // Mỗi lần nâng cấp tăng thêm 2 phòng thủ
        userDefense += upgradeAmount;
    }
    public void Heal(int healAmount) // Hồi máu
    {
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp; // Không được vượt quá máu tối đa
        }
        Debug.Log("Healed " + healAmount + " HP. Current HP: " + currentHp);
    }
    public void RegenMana(int manaAmount) // Hồi mana
    {
        currentMana += manaAmount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana; // Không được vượt quá mana tối đa
        }
        Debug.Log("Regained " + manaAmount + " Mana. Current Mana: " + currentMana);
    }
    public void UseMana(int manaCost) // Sử dụng mana
    {
        if (manaCost > currentMana)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        currentMana -= manaCost;
        Debug.Log("Used " + manaCost + " Mana. Current Mana: " + currentMana);
    }

}
 
