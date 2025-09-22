using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Static UI")]
    public UIMenu menu;
    private bool isMenuVisible = false;

    [Header("Dynamic UI")]
    public UIHealthBar uiHealthBar;
    public UIManaBar uiManaBar;

    [Header("InputUI")]
    public UIJoystick uiJoystick;
 
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
        if (Input.GetKeyDown(KeyCode.Tab) && !isMenuVisible)
        {
            menu.ShowMainMenu();
            isMenuVisible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isMenuVisible)
        {
            menu.HideAll();
            isMenuVisible = false;
        }
    }
    public void ShowMenu()
    {
        menu.ShowMainMenu();
    }
    public void QuitGame()
    {
        menu.QuitGame();
    }
    public void ShowInput()
    {
        menu.ShowInput();
    }
    public void PlayGame(string sceneName)
    {
        menu.PlayGame(sceneName);
    }
    public void LoadGame()
    {
        menu.LoadGame();
    }
}
