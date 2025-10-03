using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance => instance ??= new DataManager();
    public PlayerData player { get; private set; }
    private DataManager(){}

    //public void SyncPlayerData(PlayerControllerTest pc) // Đồng bộ dữ liệu từ PlayerControllerTest vào PlayerData
    //{
    //    if (pc == null) return;
    //    player.userLevel = pc.level;
    //    player.currentHp = pc.currentHp;
    //    player.maxMana = pc.maxMana;
    //    player.currentMana = pc.currentMana;
    //    player.userGold = pc.gold;
    //    player.userDamage = pc.damage;
    //    player.userDefense = pc.defense;
    //    player.userPosition = pc.transform.position;
    //    player.currentExperience = pc.currentExperience;
    //    player.highScore = pc.highScore; 
    //}
    public void SaveData() // Lưu dữ liệu
    {

        PlayerPrefs.SetString("UserName", player.userName); //
        PlayerPrefs.SetInt("Level", player.userLevel);
        PlayerPrefs.SetInt("Health", player.maxHp);
        PlayerPrefs.SetInt("CurrentHealth", player.currentHp);
        PlayerPrefs.SetInt("Gold", player.userGold);
        PlayerPrefs.SetInt("HighScore", player.highScore);
        PlayerPrefs.SetInt("Experience", player.currentExperience);
        PlayerPrefs.SetInt("Damage", player.userDamage);
        PlayerPrefs.SetInt("Defense", player.userDefense);
        PlayerPrefs.SetInt("Mana", player.maxMana);
        PlayerPrefs.SetInt("CurrentMana", player.currentMana);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PosX", player.userPosition.x);
        PlayerPrefs.SetFloat("PosY", player.userPosition.y);
        PlayerPrefs.SetFloat("PosZ", player.userPosition.z);
        
        PlayerPrefs.Save();
        Debug.Log("Data Saved");       
        Debug.Log($"[SaveData] name={player.userName}, level={player.userLevel}, hp={player.currentHp}/{player.maxHp}, mp= {player.currentMana}/{player.maxMana}, pos={player.userPosition}, scene={SceneManager.GetActiveScene().name}");
    }
    public void LoadData() // Load dữ liệu
    {
        
        string name = PlayerPrefs.GetString("UserName");
        int lv = PlayerPrefs.GetInt("Level");
        int hp = PlayerPrefs.GetInt("Health");
        int gold = PlayerPrefs.GetInt("Gold");
        int score = PlayerPrefs.GetInt("HighScore");
        string sceneName = PlayerPrefs.GetString("SceneName");
        float posX = PlayerPrefs.GetFloat("PosX");
        float posY = PlayerPrefs.GetFloat("PosY");
        float posZ = PlayerPrefs.GetFloat("PosZ");
        int exp = PlayerPrefs.GetInt("Experience");
        int dmg = PlayerPrefs.GetInt("Damage");
        int def = PlayerPrefs.GetInt("Defense");
        int mp = PlayerPrefs.GetInt("Mana");
        Vector3 position = new Vector3(posX, posY, posZ);
        player = new PlayerData(name, score, dmg, def, hp, mp, lv, gold, position, exp, sceneName);
        Debug.Log($"[LoadData] name={name}, level={lv}, hp={hp}, pos={position}, scene={sceneName}");

    }
    public void CreateDefaultPlayer(string name, string sceneName, Vector3 startPos) // Tạo dữ liệu mặc định
    {
       player = new PlayerData(name, 0, 10, 5, 100, 50, 1, 0, startPos, 0, sceneName);
        Debug.Log($"Default Player Created: name={name}, scene={sceneName}, pos={startPos}");
    }
    public bool HasSave() // Kiểm tra dữ liệu đã lưu
    {
        if (player == null)
        {
            Debug.LogWarning("SaveData failed: PlayerData is null");
            return false;
        }
        else
        {           
            SaveData();
            return true;
        }
    }
    public bool HasLoad() // Kiểm tra dữ liệu đã load
    {
        if (!PlayerPrefs.HasKey("UserName"))
        {
            player = null;
            CreateDefaultPlayer("Player", "MainMenu", new Vector3(-3,4,0));
            CreateDefaultPlayer();
            Debug.LogWarning("LoadData failed: No saved data found");
            return false;
        }
        else
        {
            LoadData();
            return true;
        }
    }

    public void Init()
    {
        HasLoad();
    }

}
