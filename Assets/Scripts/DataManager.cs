using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager: MonoBehaviour
{
    public static DataManager Instance {  get; private set; }
    public PlayerData player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (player == null)
            {
                player = new PlayerData();
            }
            LoadData();           

        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        player = new PlayerData
        {
            userHp =player.highScore,
            userLevel = player.userLevel,
            userGold = player.userGold,
            highScore = player.highScore,
            userName = player.userName,
            userPositions = player.userPositions
        };
        PlayerPrefs.SetInt("HighScore", player.highScore);
        PlayerPrefs.SetString("UserName", player.userName);
        PlayerPrefs.SetFloat("UserPositionX", player.userPositions[0]);
        PlayerPrefs.SetFloat("UserPositionY", player.userPositions[1]);
        PlayerPrefs.SetFloat("UserPositionZ", player.userPositions[2]);
        PlayerPrefs.SetInt("CurrentHp", player.userHp);
        PlayerPrefs.SetInt("CurrentLevel", player.userLevel);
        PlayerPrefs.SetInt("CurrentGold", player.userGold);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Deleted");
    }

    public void LoadData()
    {
        player = new PlayerData
        {
            userHp = PlayerPrefs.GetInt("CurrentHp", player.userHp),
            userLevel = PlayerPrefs.GetInt("CurrentLevel", player.userLevel),
            userGold = PlayerPrefs.GetInt("CurrentGold", player.userGold),
            highScore = PlayerPrefs.GetInt("HighScore", player.highScore),
            userName = PlayerPrefs.GetString("UserName", player.userName),
            userPositions = new Vector3(
                PlayerPrefs.GetFloat("UserPositionX", player.userPositions[0]),
                PlayerPrefs.GetFloat("UserPositionY", player.userPositions[1]),
                PlayerPrefs.GetFloat("UserPositionZ", player.userPositions[2])
            )
        };
       
        
    }
 
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        player = new PlayerData();
        Debug.Log("All player data cleared.");
    }
}
