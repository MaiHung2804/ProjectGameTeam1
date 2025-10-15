using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }

}
