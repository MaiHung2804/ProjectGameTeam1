using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
public class UIMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject inputPanel;
    public GameObject settingPanel;
    public TMP_InputField inputField;
    public  GameObject player;

    private void Awake()
    {
        inputPanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ShowMainMenu() //tắt hết các panel khác và chỉ hiện main menu
    {
        mainMenuPanel.SetActive(true);
        inputPanel.SetActive(false);
        settingPanel.SetActive(false);
    }
    public void HideAll() //tắt hết tất cả các panel
    {
        mainMenuPanel.SetActive(false);
        inputPanel.SetActive(false);
        settingPanel.SetActive(false);
    }
    public void ShowInput() //tắt hết các panel khác và chỉ hiện input
    {
        mainMenuPanel.SetActive(false);
        inputPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
    public void ShowSetting() //tắt hết các panel khác và chỉ hiện setting
    {
        mainMenuPanel.SetActive(false);
        inputPanel.SetActive(false);
        settingPanel.SetActive(true);
    }
    public void PlayGame(string sceneName)  //khi nhấn nút PlayGame sẽ lấy tên input và load scene level1 -VY (có thể đổi)
    {
        string playerName = inputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty. Please enter a valid name.");
            return;
        }
        DataManager.Instance.CreateDefaultPlayer(playerName, sceneName, new Vector3(-3, 5, 2));
        SceneManager.LoadScene(sceneName);  
        HideAll();
        
    }
    public void LoadGame() //khi nhấn nút LoadGame sẽ load dữ liệu từ PlayerPrefs và áp dụng vào nhân vật rồi load scene đã lưu
    {
        DataManager.Instance.HasLoad();
        HideAll();
        PlayerControllerTest pc = FindObjectOfType<PlayerControllerTest>();
        SceneManager.LoadScene(DataManager.Instance.player.currentScene);

    }
    public void QuitGame() //khi nhấn nút QuitGame sẽ thoát game
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void SaveGame() //khi nhấn nút SaveGame sẽ lưu dữ liệu hiện tại của nhân vật vào PlayerPrefs
    {
        var pc = FindObjectOfType<PlayerControllerTest>();
        DataManager.Instance.SyncPlayerDatat(pc);
        DataManager.Instance.HasSave();

    }
}
