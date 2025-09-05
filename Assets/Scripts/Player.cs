using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string UserName;
    public int HighScore;
    public Vector3 UserPositions;
    public int CurrentHp;
    public int CurrentLevel;
    public int CurrentGold;
    private Rigidbody rb;
    public float speed = 5f;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UserPositions = transform.position;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(direction * speed);
        
    }
    


}
