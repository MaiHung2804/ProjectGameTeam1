using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    // Điểm, vàng, thời gian, level
    public int Score { get; private set; }
    public int Gold { get; private set; }
    public float PlayTime { get; private set; }
    public int Level { get; private set; }

    // Event để UI/Audio/VFX nghe
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnGoldChanged;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        InitGame();
        Debug.Log("GameManager StartGame");

        InputManager.Instance.Init();
        ConfigManager.Instance.Init();
        PlayerManager.Instance.Init();
        FollowingCamera.Instance.Init();
        EnemyManager.Instance.Init();

    }

    private void InitGame()
    {
        ////Score = 0;
        ////Gold = 0;
        ////Level = 1;
        //PlayTime = 0;
        //ChangeState(GameState.Playing);
    }

    //private void Update()
    //{
    //    //if (CurrentState == GameState.Playing)
    //    //{
    //    //    PlayTime += Time.deltaTime;
    //    //}
    //}



    public void EndGame()
    {
        ChangeState(GameState.GameOver);
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        OnGoldChanged?.Invoke(Gold);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
        Time.timeScale = 1f;
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        OnGameStateChanged?.Invoke(newState);
    }
}
