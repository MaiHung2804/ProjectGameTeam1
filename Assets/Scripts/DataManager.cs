using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager: MonoBehaviour
{
    public PlayerData player;
    void Start()
    {
        LoadPlayerName();
        //LoadData();
        //LoadPlayerName();
        //LoadScore();
        //LoadGold();
        //LoadPosition();
        //Debug.Log("Data Load Path: " + Application.persistentDataPath);
    }



    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", player.highScore);
        PlayerPrefs.SetString("UserName", player.userName);
        PlayerPrefs.SetFloat("UserPositionX", player.userPositions[0]);
        PlayerPrefs.SetFloat("UserPositionY", player.userPositions[1]);
        PlayerPrefs.SetFloat("UserPositionZ", player.userPositions[2]);
        PlayerPrefs.SetInt("CurrentHp", player.currentHp);
        PlayerPrefs.SetInt("CurrentLevel", player.currentLevel);
        PlayerPrefs.SetInt("CurrentGold", player.currentGold);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data Deleted");
    }

    public int LoadData()
    {
        player.currentHp = PlayerPrefs.GetInt("CurrentHp");
        player.currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        Debug.Log("CurrentHp: " + player.currentHp);
        Debug.Log("CurrentLevel: " + player.currentLevel);
        Debug.Log("Data Load Pathh: " + Application.persistentDataPath);
        return player.currentHp + player.currentLevel;
    }
    public string LoadPlayerName()
    {
        return player.userName = PlayerPrefs.GetString("UserName");    
    }
    public int LoadScore()
    {
        player.highScore = PlayerPrefs.GetInt("HighScore");
        return player.highScore;
        Debug.Log("HighScore: " + player.highScore);
    }
    public int LoadGold() 
    {
        player.currentGold = PlayerPrefs.GetInt("CurrentGold");
        return player.currentGold;
        Debug.Log("CurrentGold: " + player.currentGold);
    }
    public float LoadPosition()
    {
        player.userPositions[0] = PlayerPrefs.GetFloat("UserPositionX");
        player.userPositions[1] = PlayerPrefs.GetFloat("UserPositionY");
        player.userPositions[2] = PlayerPrefs.GetFloat("UserPositionZ");
        return player.userPositions[0] + player.userPositions[1] + player.userPositions[2];
    }
}
