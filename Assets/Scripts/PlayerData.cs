using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData
{
    public string userName;
    public int highScore;
    public Vector3 userPositions;


    public int currentHp;
    public int currentLevel;
    public int currentGold;

   

    private void UpdatePosition(Vector3 position)
    {
        userPositions = position;
        
    }
}
 
