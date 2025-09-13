using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData 
{
    public string userName;
    public int highScore;
    public int userDamage;
    public int userHp;
    public int currentHp;
    public int userLevel;
    public int currentLevel;
    public int userGold;
    public float userPositionX;
    public float userPositionY;
    public float userPositionZ;
    public int currentExperience;
    public int experienceToNextLevel; // Kinh nghiệm cần để lên cấp tiếp theo
    public PlayerData(string name, int level, int gold, int hp, int score, Vector3 position, int experience, int userDamage)
    {
        this.userName = name;
        this.userLevel = level;
        this.userGold = gold;
        this.userHp = hp;
        this.highScore = score;
        this.userPositionX = position.x;
        this.userPositionY = position.y;
        this.userPositionZ = position.z;
        this.currentExperience = experience;
        this.userDamage = userDamage;
    }
    public Vector3 GetPosition() // Lấy vị trí
    {
        return new Vector3(userPositionX, userPositionY, userPositionZ);
    }  
    public void LevelUp() // Lên cấp
    {
        userLevel++;
        currentLevel = userLevel;
        Debug.Log("Level Up! New Level: " + userLevel + ", New HP: " + userHp);
    }
    public void AddGold(int amount) // Thêm vàng
    {
        userGold += amount;
        Debug.Log("Added " + amount + " gold. Total Gold: " + userGold);
    }
    public void TakeDamage(int damage) // Nhận sát thương
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
        Debug.Log("Took " + damage + " damage. Current HP: " + currentHp);
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
        
    }
    public void HealthUp(int upgradeAmount) // Nâng cấp máu
    {
        upgradeAmount = upgradeAmount * 10; // Mỗi lần nâng cấp tăng thêm 10 máu
        userHp += upgradeAmount;
        currentHp = userHp; // Hồi máu đầy khi nâng cấp
        Debug.Log("Health upgraded by " + upgradeAmount + ". New HP: " + userHp);
    }

}
 
