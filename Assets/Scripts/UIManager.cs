using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public DataManager dataManager;

    public void PlayButton(string sceneName)
    {
       SceneManager.LoadScene(sceneName);
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void SaveData()
    {
        dataManager.SaveData();
    }
}
