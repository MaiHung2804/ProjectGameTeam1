using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataManager: MonoBehaviour
{
    public static DataManager Instance;
    public PlayerData player;
    public Transform playerTransform;

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy() // Hủy đăng ký sự kiện khi đối tượng bị hủy
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        if (PlayerPrefs.HasKey("PosX"))
        {
            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            Vector3 newPos = new Vector3(x, y, z);

            if (playerTransform != null)
            {
                playerTransform.position = newPos;
                Debug.Log("Đã load vị trí Player: " + newPos);
            }
        } 
    }
    private void OnApplicationQuit() // Khi thoát game tự động lưu dữ liệu
    {
        SaveData();
    }
    public void SaveData() // Lưu dữ liệu
    {
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
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Deleted");
    }
    public void LoadData() // Load dữ liệu
    {
        string name = PlayerPrefs.GetString("UserName", "Player");
        int level = PlayerPrefs.GetInt("Level", 1);
        int health = PlayerPrefs.GetInt("Health", 100);
        int gold = PlayerPrefs.GetInt("Gold", 0);
        int score = PlayerPrefs.GetInt("HighScore", 0);
        string sceneName = PlayerPrefs.GetString("SceneName");
        float posX = PlayerPrefs.GetFloat("PosX", 0);
        float posY = PlayerPrefs.GetFloat("PosY", 0);
        float posZ = PlayerPrefs.GetFloat("PosZ", 0);
        int experience = PlayerPrefs.GetInt("Experience", 0);
        int damage = PlayerPrefs.GetInt("Damage", 10);
        Vector3 position = new Vector3(posX, posY, posZ);
        player = new PlayerData(name, level, gold, health, score, position, experience, damage);
        
        playerTransform.position = player.GetPosition();
        SceneManager.LoadScene(sceneName);

    }
public void ClearData() // Xóa tất cả dữ liệu
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All player data cleared.");
    }
}
