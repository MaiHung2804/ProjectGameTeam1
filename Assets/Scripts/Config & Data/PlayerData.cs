using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerData: UnitData
{
    protected string userName;
    protected int highScore;
    protected int damage;
    protected float defense;
    protected int maxHp;
    protected int currentMana;
    protected int maxMana;
    protected int level;
    protected int gold;
    protected int currentExp;
    protected int maxExp;
    protected float maxSpeed;
    protected string currentScene;
    protected Vector3 userLastPosition;

    // Constructor de khoi tao du lieu nguoi choi
    public PlayerData(UnitConfig config): base(config)
    {
        this.userName = config.Name;
        this.highScore = 0;
        this.damage = config.BaseDamage;
        this.defense = config.BaseDefense;
        this.maxHp = config.BaseHp;
        this.currentMana = config.BaseMana;
        this.maxMana = config.BaseMana;
        this.level = config.BaseLevel;
        this.gold = config.BaseGold;
        this.currentExp = 0;
        this.maxSpeed = config.BaseMaxSpeed;
        this.maxExp = level * 100;
        this.userLastPosition = new Vector3(-3, 5, 12);
        this.currentScene = "StartScene";
    }


    //public void SetPlayerData(string name, int score, int dmg, int def, int hp, int mp, int lv,
    //    int gold, float speed, Vector3 position, int exp, string scene, int currentHp, int currentMp, int currentExp)
    //{
    //    this.userName = name;
    //    this.highScore = score;
    //    this.damage = dmg;
    //    this.defense = def;
    //    this.maxHp = hp;
    //    this.currentHp = hp;
    //    this.maxMana = mp;
    //    this.currentMana = mp;
    //    this.gold = gold;
    //    this.maxSpeed = speed;
    //    this.userPosition = position;
    //    this.currentExp = currentExp;
    //    this.level = lv;
    //    this.maxExp = lv * 100;
    //    this.currentScene = scene;

    //    this.currentHp = (currentHp > 0) ? currentHp : hp;
    //    this.currentMana = (currentMp > 0) ? currentMp : mp;
    //    this.currentExp = (currentExp > 0) ? currentExp : 0;
    //}

    //public PlayerData() // Constructor mac dinh
    //{
    //    this.userName = "Player";
    //    this.highScore = 0;
    //    this.damage = 10;
    //    this.defense = 5;
    //    this.maxHp = 100;
    //    this.currentHp = maxHp;
    //    this.maxMana = 50;
    //    this.currentMana = maxMana;
    //    this.userGold = 0;
    //    this.userPosition = new Vector3(-3, 5, 2);
    //    this.current = 0;
    //    this.level = 1;
    //    this.maxExp = level * 100;
    //    this.currentScene = "StartScene";
    //}

    public override string Name { get => userName; set => userName = value; } // Ten don vi
    public override int Level { get => level; set => level = value; }
    public override int Damage { get => damage; set => damage = value; }
    public override float Defense { get => defense; set => defense = value; }
    public override int MaxMana { get => maxMana; set => maxMana = value; }
    public override int Mana { get => currentMana; set => currentMana = value; }
    public override float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public override int Gold { get => gold; set => gold = value; }
    public override int MaxHp { get => maxHp; set => maxHp = value; }

    public int HighScore { get => highScore; set => highScore = value; }
    public int CurrentExp { get => currentExp; set => currentExp = value; }
    public int MaxExp { get => maxExp; set => maxExp = value; }
    public string CurrentScene { get => currentScene; set => currentScene = value; }
    public Vector3 UserLastPosition { get => userLastPosition; set => userLastPosition = value; }

    //public Vector3 GetPosition() // Lấy vị trí
    //{
    //    return userPosition;
    //}
    //public void SetPosition(Vector3 position) // Đặt vị trí
    //{
    //    userPosition = position;
    //}

    #region KHU VUC TEST KHI CHUA CO COMPONENT RIENG

    public void LevelUp() // TAM THOI DE O DAY VI CHUA CO COMPONENT RIENG
    {
        level++;
        DamageUpg(1);
        HealthUp(1);
        DefenseUpg(1);
        ManaUp(1);
        maxExp = level * 3;
        //Debug.Log("Level Up! New Level: " + level + ", New HP: " + maxHp);
    }
    public void AddGold(int amount) // Thêm vàng
    {
        gold += amount;
        //Debug.Log("Added " + amount + " gold. Total Gold: " + userGold);
    }

    //public void TakeDamage(int damage) // Nhận sát thương
    //{
    //    int damageTaken = damage - defense;
    //    if (damageTaken < 0) damageTaken = 0;

    //    currentHp -= damageTaken;
    //    if (currentHp < 0) currentHp = 0;

    //    Debug.Log($"{userName} took {damageTaken} damage. Current HP: {currentHp}");


    //    if (currentHp <= 0)
    //        Die();
    //}
    //public void Die() // Chết
    //{

    //    Debug.Log(userName + " has died.");

    //}

    

    public void TakeExperience(int experience) // TAM THOI DE O DAY VI CHUA CO COMPONENT RIENG
    {
        currentExp += experience;
        maxExp = level * 100;

        if (currentExp >= maxExp)
        {
            LevelUp();
        }

    }

    private void DamageUpg(int upgradeAmount) // Nâng cấp sát thương
    {
        upgradeAmount = upgradeAmount * 5; // Mỗi lần nâng cấp tăng thêm 5 sát thương
        damage += upgradeAmount;
    }
    private void HealthUp(int upgradeAmount) // Nâng cấp máu
    {
        upgradeAmount = upgradeAmount * 10; // Mỗi lần nâng cấp tăng thêm 10 máu
        maxHp += upgradeAmount;
        currentHp = maxHp; // Hồi máu đầy khi nâng cấp
        //Debug.Log("Health upgraded by " + upgradeAmount + ". New HP: " + maxHp);
    }
    private void ManaUp(int upgradeAmount) // Nâng cấp mana
    {
        upgradeAmount = upgradeAmount * 5; // Mỗi lần nâng cấp tăng thêm 5 mana
        maxMana += upgradeAmount;
        currentMana = maxMana; // Hồi mana đầy khi nâng cấp
        //Debug.Log("Mana upgraded by " + upgradeAmount + ". New Mana: " + maxMana);
    }
    public void DefenseUpg(int upgradeAmount) // Nâng cấp phòng thủ
    {
        upgradeAmount = upgradeAmount * 2; // Mỗi lần nâng cấp tăng thêm 2 phòng thủ
        defense += upgradeAmount;
    }
    public void Heal(int healAmount) // Hồi máu
    {
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp; // Không được vượt quá máu tối đa
        }
        //Debug.Log("Healed " + healAmount + " HP. Current HP: " + currentHp);
    }
    public void RegenMana(int manaAmount) // Hồi mana
    {
        currentMana += manaAmount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana; // Không được vượt quá mana tối đa
        }
        //Debug.Log("Regained " + manaAmount + " Mana. Current Mana: " + currentMana);
    }
    public void UseMana(int manaCost) // Sử dụng mana
    {
        if (manaCost > currentMana)
        {
            //Debug.Log("Not enough mana!");
            return;
        }
        currentMana -= manaCost;
        //Debug.Log("Used " + manaCost + " Mana. Current Mana: " + currentMana);
    }

    #endregion
}

