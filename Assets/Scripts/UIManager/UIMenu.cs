using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;
public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputPanel.SetActive(false);
    }
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        inputPanel.SetActive(false);
    }
    public void HideAll()
    {
        mainMenuPanel.SetActive(false);
        inputPanel.SetActive(false);
    }
    public void ShowInput()
    {
        mainMenuPanel.SetActive(false);
        inputPanel.SetActive(true);
    }
    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        HideAll();  
    }
    public void LoadGame()
    {
        dataManager.LoadData();
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void SetPlayerName()
    {
        string playerName = inputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            dataManager.Username = playerName;
            Debug.Log("Player name set to: " + dataManager.Username);
            ShowMainMenu();
        }
        else
        {
            Debug.LogWarning("Player name cannot be empty!");
        }
    }

}
