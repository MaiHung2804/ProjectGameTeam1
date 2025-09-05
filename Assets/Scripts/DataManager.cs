using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager: MonoBehaviour
{
    public Player player;
    void Start()
    {   
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", player.HighScore);
        PlayerPrefs.SetString("UserName", player.UserName);
        PlayerPrefs.SetFloat("UserPositionX", player.UserPositions[0]);
        PlayerPrefs.SetFloat("UserPositionY", player.UserPositions[1]);
        PlayerPrefs.SetFloat("UserPositionZ", player.UserPositions[2]);
        PlayerPrefs.SetInt("CurrentHp", player.CurrentHp);
        PlayerPrefs.SetInt("CurrentLevel", player.CurrentLevel);
        PlayerPrefs.SetInt("CurrentGold", player.CurrentGold);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }

    public void LoadData()
    {
        player.HighScore = PlayerPrefs.GetInt("HighScore");
        player.UserName = PlayerPrefs.GetString("UserName");
        player.UserPositions[0] = PlayerPrefs.GetFloat("UserPositionX"); 
        player.UserPositions[1] = PlayerPrefs.GetFloat("UserPositionY");
        player.UserPositions[2] = PlayerPrefs.GetFloat("UserPositionZ");
        player.CurrentHp = PlayerPrefs.GetInt("CurrentHp");
        player.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        player.CurrentGold = PlayerPrefs.GetInt("CurrentGold");
        Debug.Log("UserName: " + player.UserName);
        Debug.Log("HighScore: " + player.HighScore);
        Debug.Log("UserPosition: " + player.UserPositions[0] + "," + player.UserPositions[1] + "," + player.UserPositions[2]);
        Debug.Log("CurrentHp: " + player.CurrentHp);
        Debug.Log("CurrentLevel: " + player.CurrentLevel);
        Debug.Log("CurrentGold: " + player.CurrentGold);
        Debug.Log("Data Load Pathh: " + Application.persistentDataPath);

    }
}
