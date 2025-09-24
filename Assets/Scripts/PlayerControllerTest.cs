using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
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
        // Nếu load dữ liệu thì apply vào nhân vật
        if (DataManager.Instance.player != null)
        {
            ApplyData(DataManager.Instance.player);
        }
    }
    void Update()
    {
        // Test lấy sát thương
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UseMana();
        }
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
        level = data.userLevel;
    }


    public void TakeDamage() //Hàm mẫu dùng để test nhân vật nhận sát thương
    {
        PlayerData data = DataManager.Instance.player;
        if (data == null) return;
        int damageAmount = 20; // Giả sử sát thương cố định là 20
        data.TakeDamage(damageAmount);
        // update UI ngay lập tức
        FindObjectOfType<UIDataPlayer>()?.UpdateUI();
    }
    public void UseMana() //Hàm mẫu dùng để test nhân vật sử dụng mana
    {
        PlayerData data = DataManager.Instance.player;
        if (data == null) return;
        int manaAmount = 10; // Giả sử sát thương cố định là 20
        data.UseMana(manaAmount);
        // update UI ngay lập tức
        FindObjectOfType<UIDataPlayer>()?.UpdateUI();
    }
}
