using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }   
    private bool isMenuVisible;

    [Header("Static UI")]
    public UIMenu menu;

    [Header("Dynamic UI")]
    public UIDataPlayer uiDataPlayer;

 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (menu != null)
            {
                isMenuVisible = !menu.settingPanel.activeSelf;
                menu.settingPanel.SetActive(isMenuVisible);
                Time.timeScale = isMenuVisible ? 0 : 1; 
            }
        } // Nhan Tab show/hide menu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (menu != null)
            {
                isMenuVisible = !menu.mainMenuPanel.activeSelf;
                menu.mainMenuPanel.SetActive(isMenuVisible);
                Time.timeScale = isMenuVisible ? 0 : 1;
            }
        }

    }
    public void ShowMenu()  // gọi đến hàm ShowMainMenu trong UIMenu 
    {
        menu.ShowMainMenu();
    } 
    public void QuitGame()  //gọi đến hàm QuitGame trong UIMenu
    {
        menu.QuitGame();
    }
    public void ShowInput()  //gọi đến hàm ShowInput trong UIMenu
    {
        menu.ShowInput();
    }
    public void PlayGame(string sceneName) //gọi đến hàm PlayGame trong UIMenu
    {   
        menu.PlayGame(sceneName);      
    }
    public void LoadGame() //gọi đến hàm LoadGame trong UIMenu
    {
        menu.LoadGame();
    }
    public void SaveGame() //gọi đến hàm SaveGame trong UIMenu
    {
        menu.SaveGame();
    }
}
