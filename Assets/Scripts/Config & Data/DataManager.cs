using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance => instance ??= new DataManager();

    public PlayerData player;
    private DataManager() {  }

    // ✅ Tạo dữ liệu mới khi bắt đầu New Game
    public void NewPlayerData()
    {
        player = new PlayerData();
        Debug.Log($"New Player: {player.userName} | HP {player.currentHp}/{player.maxHp} | " +
           $"MP {player.currentMana}/{player.maxMana} | " +
           $"EXP {player.currentExperience}/{player.maxExp} | " +
           $"Level {player.currentLevel} | " +
           $"Gold {player.userGold} | " +
           $"Scene {player.currentScene} | " +
           $"Position {player.userPosition}");
    }

    // ✅ Lưu dữ liệu người chơi hiện tại
    public void SaveData()
    {


        if (player == null)
        {
            Debug.LogWarning("❌ No player data to save!");
            return;
        }

        PlayerPrefs.SetString("Player", player.userName);
        PlayerPrefs.SetInt("Health", player.maxHp);
        PlayerPrefs.SetInt("CurrentHealth", player.currentHp);
        PlayerPrefs.SetInt("Gold", player.userGold);
        PlayerPrefs.SetInt("HighScore", player.highScore);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);


        PlayerPrefs.SetFloat("PosX", player.userPosition.x);
        PlayerPrefs.SetFloat("PosY", player.userPosition.y);
        PlayerPrefs.SetFloat("PosZ", player.userPosition.z);


        PlayerPrefs.SetInt("Experience", player.currentExperience);
        PlayerPrefs.SetInt("MaxExp", player.maxExp);
        PlayerPrefs.SetInt("Level", player.currentLevel);
        PlayerPrefs.SetInt("Damage", player.userDamage);
        PlayerPrefs.SetInt("Defense", player.userDefense);
        PlayerPrefs.SetInt("Mana", player.maxMana);
        PlayerPrefs.SetInt("CurrentMana", player.currentMana);
        //string json = JsonUtility.ToJson(player);
        //PlayerPrefs.SetString(PlayerSlotSavekey1, json);

        PlayerPrefs.Save();

        Debug.Log($"💾 Saved: {player.userName} | HP {player.currentHp}/{player.maxHp} | " +
            $"MP {player.currentMana}/{player.maxMana} | " +
            $"EXP {player.currentExperience}/{player.maxExp } | " +
            $"Level {player.currentLevel} | " +
            $"Gold {player.userGold} | " +
            $"Scene {player.currentScene} | " +
            $"Position {player.userPosition}");

    }


    // ✅ Load dữ liệu đã lưu
    public bool LoadData()
    {
        if (PlayerPrefs.HasKey("Player"))
        {

            //string json = PlayerPrefs.GetString(PlayerSlotSavekey1);
            //player = JsonUtility.FromJson<PlayerData>(json);
            //Debug.Log($"✅ Loaded: {player.userName} | HP {player.currentHp}/{player.maxHp} | " +
            //    $"MP {player.currentMana}/{player.maxMana} | " +
            //    $"EXP {player.currentExperience}/{player.experienceToNextLevel} | " +
            //    $"Level {player.currentLevel} | " +
            //    $"Gold {player.userGold} | " +
            //    $"Scene {player.currentScene} | " +
            //    $"Position {player.userPosition}");

            string name = PlayerPrefs.GetString("Player");
            int lv = PlayerPrefs.GetInt("Level");
            int hp = PlayerPrefs.GetInt("Health");
            int currentHp = PlayerPrefs.GetInt("CurrentHealth");
            int gold = PlayerPrefs.GetInt("Gold");
            int score = PlayerPrefs.GetInt("HighScore");
            string sceneName = PlayerPrefs.GetString("SceneName");
            float posX = PlayerPrefs.GetFloat("PosX");
            float posY = PlayerPrefs.GetFloat("PosY");
            float posZ = PlayerPrefs.GetFloat("PosZ");
            int currentExp = PlayerPrefs.GetInt("Experience");
            int exp = PlayerPrefs.GetInt("MaxExp");
            int dmg = PlayerPrefs.GetInt("Damage");
            int def = PlayerPrefs.GetInt("Defense");
            int mp = PlayerPrefs.GetInt("Mana");
            int currentMp = PlayerPrefs.GetInt("CurrentMana");
            Vector3 position = new Vector3(posX, posY, posZ);

            player = new PlayerData(name, score, dmg, def, hp, mp, lv, gold, position, exp, sceneName, currentHp, currentMp, currentExp);

            Debug.Log($"✅ [LoadData] {name}, Level {lv}, HP {currentHp}/{hp}, MP {currentMp}/{mp}, Scene {sceneName}, Position {position}, Exp {currentExp}/{exp}");
            return true;
        }
        else
        {
            player = new PlayerData();
            Debug.LogWarning("⚠️ No saved data found. Created default player.");
            return false;
        }


     }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey("Player");
    }

    public bool HasLoad()
    {
        if (HasSave())
        {
            LoadData();
            return true;
        }
        return false;
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        
        Debug.Log("🗑️ All data deleted successfully.");
    }
}
