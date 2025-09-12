using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData 
{
    public string userName;
    public int highScore;
    public Vector3 userPositions;
    public int userHp;
    public int currentHp;
    public int userLevel;
    public int currentLevel;
    public int userGold;

    public PlayerData()
    {
        userName = "Guest";
        userLevel = 1;
        userGold = 0;
        userHp = 100;
        highScore = 0;
        userPositions = Vector3.zero;
    }
    private void UpdatePosition(Vector3 position)
    {
        userPositions = position;
        
    }

    public void UpdateHp() 
    {
        currentHp = userHp;

    }
}
 
