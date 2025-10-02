using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerControllerTest instance;
    public int level = 1;
    public int currentHp = 100;
    public int maxHp = 100;
    public int currentMana = 50;
    public int maxMana = 50;
    public int gold = 0;
    public int damage = 10;
    public int defense = 5;
    public int currentExperience = 0;
    public int highScore = 0;
    public int speed = 5;

    private void Awake()
    {
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject); 
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Nếu load dữ liệu thì apply vào nhân vật
        if (DataManager.Instance.player != null)
        {
            ApplyData(DataManager.Instance.player);
        }
    }
    void Update()
    {


        Vector2 moveInput = InputManager.Instance.GetMoveInput();
        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);

    }

    public void ApplyData(PlayerData data) // Áp dụng dữ liệu từ PlayerData vào nhân vật
    {
        level = data.userLevel;
        currentHp = data.currentHp;
        maxHp = data.maxHp;
        currentMana = data.currentMana;
        maxMana = data.maxMana;
        gold = data.userGold;
        damage = data.userDamage;
        defense = data.userDefense;
        transform.position = data.GetPosition();
        currentExperience = data.currentExperience;
        highScore = data.highScore;
    }


}
