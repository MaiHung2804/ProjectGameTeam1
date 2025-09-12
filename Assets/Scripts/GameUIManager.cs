using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text playerNameText;

    void Start()
    {
        
        playerNameText.text = DataManager.Instance.player.userName;
    }

  
}
