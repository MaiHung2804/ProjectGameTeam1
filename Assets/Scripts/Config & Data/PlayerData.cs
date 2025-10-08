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
    public int currentLevel;
    public int userGold;
    public int currentExperience;
    public int maxExp;
    public string currentScene;
    public Vector3 userPosition;

    // Constructor để khởi tạo dữ liệu người chơi (thiếu gì nhắn em để thêm)
    public PlayerData(string name, int score, int dmg, int def, int hp, int mp, int lv,
        int gold, Vector3 position, int exp, string scene, int currentHp, int currentMp, int currentExp)
    {
        this.userName = name;
        this.highScore = score;
        this.userDamage = dmg;
        this.userDefense = def;
        this.maxHp = hp;
        this.currentHp = hp;
        this.maxMana = mp;
        this.currentMana = mp;
        this.userGold = gold;
        this.userPosition = position;
        this.currentExperience = currentExp;
        this.currentLevel = lv;
        this.maxExp = lv * 100;
        this.currentScene = scene;

        this.currentHp = (currentHp > 0) ? currentHp : hp;
        this.currentMana = (currentMp > 0) ? currentMp : mp;
        this.currentExperience = (currentExp > 0) ? currentExp : 0;
    }
    public PlayerData() // Constructor mặc định
    {
        this.userName = "Player";
        this.highScore = 0;
        this.userDamage = 10;
        this.userDefense = 5;
        this.maxHp = 100;
        this.currentHp = maxHp;
        this.maxMana = 50;
        this.currentMana = maxMana;
        this.userGold = 0;
        this.userPosition = new Vector3(-3, 5, 2);
        this.currentExperience = 0;
        this.currentLevel = 1;
        this.maxExp = currentLevel * 100;
        this.currentScene = "StartScene";
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
        currentLevel++;
        DamageUpg(1);
        HealthUp(1);
        DefenseUpg(1);
        ManaUp(1);
        maxExp = currentLevel * 3;
        Debug.Log("Level Up! New Level: " + currentLevel + ", New HP: " + maxHp);
    }
    public void AddGold(int amount) // Thêm vàng
    {
        userGold += amount;
        Debug.Log("Added " + amount + " gold. Total Gold: " + userGold);
    }
    public void TakeDamage(int damage) // Nhận sát thương
    {
        int damageTaken = damage - userDefense;
        if (damageTaken < 0) damageTaken = 0;

        currentHp -= damageTaken;
        if (currentHp < 0) currentHp = 0;

        Debug.Log($"{userName} took {damageTaken} damage. Current HP: {currentHp}");


        if (currentHp <= 0)
            Die();
    }
    public void Die() // Chết
    {

        Debug.Log(userName + " has died.");

    }
    public void TakeExperience(int experience)
    {
        currentExperience += experience;
        maxExp = currentLevel * 100;

        if (currentExperience >= maxExp)
        {
            LevelUp();
            currentExperience -= maxExp;
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

