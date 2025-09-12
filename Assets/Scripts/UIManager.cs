using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public bool isUIVisible = true;
    public DataManager dataManager;
    public GameObject CanvasMenu;
    public GameObject CanvasInput;
    public TMP_InputField playername;
    public PlayerData playerData;

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Nhấn Tab hiện UI Menu
        {
            isUIVisible = true;
            CanvasMenu.SetActive(isUIVisible);
        }
   
    }

    public void NewGameButton() // Khi ấn vào nút Newgame, đặt các giá trị về ban đầu
    {
        dataManager.DeleteData();
        playerData.currentHp = 100;
        playerData.currentLevel = 1;
        playerData.currentGold = 0;
        playerData.highScore = 0;
        ShowCanvasInput();        
    } 
    public void PlayButton(string sceneName) // Ấn vào Play load scene lv1
    {
        //SceneManager.LoadScene(sceneName);
        Debug.Log("Current Hp: " + playerData.currentHp);
        Debug.Log("Current Level: " + playerData.currentLevel);
        Debug.Log("Current Gold: " + playerData.currentGold);
        Debug.Log("High Score: " + playerData.highScore);
        Debug.Log("User Name: " + playerData.userName);
        Debug.Log("Player Name" + dataManager.LoadPlayerName());
        dataManager.LoadPlayerName();
        Debug.Log("Player Name" + dataManager.LoadPlayerName());
    }
    public void ExitButton() // Ấn vào Exit thoát game
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void SaveData() // Ấn vào Save để lưu dữ liệu
    {
        dataManager.SaveData();
    }

    public void InputUserName() // Lấy dữ liệu từ InputField và gán vào UserName
    {
        playerData.userName = playername.text;
        PlayerPrefs.SetString("UserName", playerData.userName);
    }
    public void ShowCanvasInput() // Hiện CanvasInput để nhập tên
    {
        CanvasMenu.SetActive(false);
        CanvasInput.SetActive(true);
    }
    //public void Continue()
    //{
    //    PlayerData player = dataManager.player;
    //    dataManager.LoadData();
    //    player.userPositions = new Vector3(PlayerPrefs.GetFloat("UserPositionX"), PlayerPrefs.GetFloat("UserPositionY"), PlayerPrefs.GetFloat("UserPositionZ"));
    //    SceneManager.LoadScene("Level" + player.currentLevel);
    //    player.transform.position = player.userPositions;
    //    Debug.Log("Player Position: " + player.userPositions);

    //}

 
}
