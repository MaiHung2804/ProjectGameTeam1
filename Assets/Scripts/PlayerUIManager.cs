using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    public PlayerMoveComponent playerMoveComponent;
    public TMP_Text playerNameText;
    public Joystick joystick;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
        playerNameText.text = DataManager.Instance.player.userName;
    }

    private void Update()
    {
        Move();
    }
    public void Attack()
    {        

        Debug.Log("Player UI Attack");
    }
    public void Move()
    {   
        
       

    }

}
