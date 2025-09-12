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
        else if (Input.GetKeyDown(KeyCode.Escape)) // Nhấn Esc ẩn UI Menu
        {
            isUIVisible = false;
            CanvasMenu.SetActive(isUIVisible);
        }
    }

    public void NewGameButton() // Khi ấn vào nút Newgame, đặt các giá trị về ban đầu
    {
        dataManager.DeleteData();
        ShowCanvasInput();        
    } 
    public void PlayButton(string sceneName) // Ấn vào Play load scene lv1
    {
       DataManager.Instance.player.userName = playername.text;
       SceneManager.LoadScene(sceneName);
        Debug.Log("Play button clicked, loading scene: " + sceneName);

    }
    public void ExitButton() // Ấn vào Exit thoát game
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void SaveData() // Ấn vào Save để lưu dữ liệu
    {
        DataManager.Instance.SaveData();        
        Debug.Log("Data is saved");
    }


    public void ShowCanvasInput() // Hiện CanvasInput để nhập tên
    {
        CanvasMenu.SetActive(false);
        CanvasInput.SetActive(true);
    }

    public void BackToMenu(string sceneName) // Quay về Menu chính
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Back to Menu");
    }

    public void LoadGame() // Load dữ liệu khi ấn SaveGame
    {
        DataManager.Instance.LoadData();
        SceneManager.LoadScene("Level1");
        Debug.Log("Data is loaded");
    }

}
