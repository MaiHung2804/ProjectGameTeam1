using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTest : MonoBehaviour
{
    public static GameManagerTest Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }

}
