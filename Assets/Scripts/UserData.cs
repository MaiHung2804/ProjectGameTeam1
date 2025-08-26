using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public string userName;
    public int userScore;
    public int currentHp;
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && (Input.GetKeyDown(KeyCode.LeftControl)))
        {
            SaveGame();
        }
    }
    void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.SetInt("UserScore", userScore);
        PlayerPrefs.SetInt("CurrentHp", currentHp);        
        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            userName = PlayerPrefs.GetString("UserName");
            userScore = PlayerPrefs.GetInt("UserScore");
            currentHp = PlayerPrefs.GetInt("CurrentHp");
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No saved game found");
        }
    }

}
