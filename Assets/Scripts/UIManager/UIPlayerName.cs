using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIPlayerName : MonoBehaviour
{   
    public TextMeshProUGUI playerNameText;
    void Start()
    {
        playerNameText.text = PlayerPrefs.GetString("PlayerName", "Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
