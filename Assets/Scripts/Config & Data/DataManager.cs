using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance => instance ??= new DataManager();
    private const string SaveKey = "Player_Data";
    public PlayerData player { get; private set; }
    public Transform playerTransform;
    public GameObject playerPrefab;
    public string Username { get; set; }


    private DataManager() 
    {
        LoadData();
    }

    
    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{        
    //    if (playerTransform == null)
    //    {
    //        GameObject found = GameObject.FindGameObjectWithTag("Player");
    //        if (found != null)
    //        {
    //            playerTransform = found.transform;
    //            Debug.Log("PlayerTransform được gán lại từ scene.");
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Không tìm thấy Player trong scene!");
    //            return;
    //        }
    //    }

    //    // Nếu player data chưa có thì tạo mặc định
    //    if (player == null)
    //    {
    //        player = new PlayerData("Player", 1, 0, 100, 0, 0, playerTransform.position, 5, 10);
    //    }

    //    // Load vị trí từ PlayerPrefs nếu có
    //    Vector3 newPos;
    //    if (PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("PosY") && PlayerPrefs.HasKey("PosZ"))
    //    {
    //        newPos = new Vector3(
    //            PlayerPrefs.GetFloat("PosX"),
    //            PlayerPrefs.GetFloat("PosY"),
    //            PlayerPrefs.GetFloat("PosZ")
    //        );
    //    }
    //    else
    //    {
    //        newPos = playerTransform.position; // Vị trí mặc định nếu không có dữ liệu lưu
    //    }
    //    playerTransform.position = newPos;
    //    player.SetPosition(newPos);
    //    Debug.Log("Scene Loaded: " + scene.name + ", Player Position: " + playerTransform.position);
    //}
    //private void OnApplicationQuit() // Khi thoát game tự động lưu dữ liệu
    //{
    //    if (player != null)
    //    {
    //        SaveData();
    //    }
    //}
    public void SaveData() // Lưu dữ liệu
    {
        if (player == null || playerTransform == null)
        {
            Debug.LogWarning("⚠ SaveData failed: " + player +" hoặc " + playerTransform + " bị null");
            return;
        }
        PlayerPrefs.SetString("UserName", player.userName);
        PlayerPrefs.SetInt("Level", player.userLevel);
        PlayerPrefs.SetInt("Health", player.userHp);
        PlayerPrefs.SetInt("Gold", player.userGold);
        PlayerPrefs.SetInt("HighScore", player.highScore);
        PlayerPrefs.SetInt("Experience", player.currentExperience);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("PosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PosZ", playerTransform.position.z);
        PlayerPrefs.SetInt("Damage", player.userDamage);
        PlayerPrefs.SetInt("Defense", player.userDefense);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
        Debug.Log("Scene: " + SceneManager.GetActiveScene().name + ", Position: " + playerTransform.position);
    }
    public void LoadData() // Load dữ liệu
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string name = PlayerPrefs.GetString("UserName", "Player");
            int level = PlayerPrefs.GetInt("Level", 1);
            int health = PlayerPrefs.GetInt("Health", 100);
            int gold = PlayerPrefs.GetInt("Gold", 0);
            int score = PlayerPrefs.GetInt("HighScore", 0);
            string sceneName = PlayerPrefs.GetString("SceneName", "Level1");
            float posX = PlayerPrefs.GetFloat("PosX", 0);
            float posY = PlayerPrefs.GetFloat("PosY", 0);
            float posZ = PlayerPrefs.GetFloat("PosZ", 0);
            int experience = PlayerPrefs.GetInt("Experience", 0);
            int dmg = PlayerPrefs.GetInt("Damage", 10);
            int def = PlayerPrefs.GetInt("Defense", 5);
            int mana = PlayerPrefs.GetInt("Mana", 50);
            Vector3 position = new Vector3(posX, posY, posZ);

            player = new PlayerData(name, level, gold, health, score, experience, position, def, dmg, mana);
            SceneManager.LoadScene(sceneName);
            Debug.Log("Scene Loaded: " + sceneName);
        }
        else
        {
            player = new PlayerData("Player", 1, 0, 100, 0, 0, Vector3.zero, 5, 10, 50);
            Debug.Log("No saved data found, created new player data.");

        }
    }
    public void ResetData() // Xóa tất cả dữ liệu
    {
        player = new PlayerData("Player", 1, 0, 100, 0, 0, Vector3.zero, 5, 10, 50);
        SaveData();
    }   

}
