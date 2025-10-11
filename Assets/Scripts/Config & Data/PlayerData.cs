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

    // Constructor 
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

}

