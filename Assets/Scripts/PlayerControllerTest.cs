using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    private PlayerData playerData;
    public HealthCompentTest healthCompentTest;
    private Rigidbody rb;

    public float speed = 5;

    void Awake()
    {
        healthCompentTest = GetComponent<HealthCompentTest>();
        playerData = DataManager.Instance.player;
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        healthCompentTest.SetHealth();
        GetTransform();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDame();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
           Heal();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.angularVelocity = movement * speed;
        SetTransform();


    }

    public void TakeDame()
    {
        healthCompentTest.TakeDamage(10);

    }
    public void Heal()
    {
        healthCompentTest.Heal(10);


    }
    public void SetTransform()
    {
        if (DataManager.Instance.player != null)
        {
            DataManager.Instance.player.userPosition = transform.position;
        }
    }
    public void GetTransform()
    {
        if (DataManager.Instance.player != null)
        {
            transform.position = DataManager.Instance.player.userPosition;
        }
    }

}
