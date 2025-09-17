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
            Time.timeScale = 0f; // Tạm dừng game khi mở menu
            CanvasMenu.SetActive(isUIVisible);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // Nhấn Esc ẩn UI Menu
        {
            isUIVisible = false;
            Time.timeScale = 1f; // Tiếp tục game khi đóng menu
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
        UserName();
        SceneManager.LoadScene(sceneName);
        Debug.Log("Play button clicked, loading scene: " + sceneName);

    }
    public void UserName()
    {
        string name = playername.text;
        DataManager.Instance.player.userName = name;
    }
    public void ExitButton() // Ấn vào Exit thoát game
    {
        Application.Quit();
    }
    public void SaveData() // Ấn vào Save để lưu dữ liệu
    {
        DataManager.Instance.SaveData();        
    }
    public void ShowCanvasInput() // Hiện CanvasInput để nhập tên
    {
        CanvasMenu.SetActive(false);
        CanvasInput.SetActive(true);
    }
    public void BackToMenu(string sceneName) // Quay về Menu chính
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadGameButton() // Load dữ liệu khi ấn LoadGame
    {
        DataManager.Instance.LoadData();
        int level = DataManager.Instance.player.userLevel;
        string sceneName = "Level" + level;
        SceneManager.LoadScene(sceneName);
        Debug.Log("Load Game button clicked, loading scene: " + sceneName);

    }

}
