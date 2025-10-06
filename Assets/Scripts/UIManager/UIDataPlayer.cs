using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIDataPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private Slider uiHealthBar;
    [SerializeField] private Slider uiManaBar;
    [SerializeField] private Slider uiExpBar;
    private PlayerData player;
    void Start()
    {
        
    }

    private void Update()
    {
        UppdateUI();
    }
   

    public void UppdateUI()
    {
        player = DataManager.Instance.player;
        if (player == null) return;
        
        playerNameText.text = player.userName;
        playerLevelText.text = player.currentLevel.ToString();
        
        uiHealthBar.maxValue = player.maxHp;
        uiHealthBar.value = player.currentHp;
        uiManaBar.maxValue = player.maxMana;
        uiManaBar.value = player.currentMana;
        uiExpBar.maxValue = player.maxExp;
        uiExpBar.value = player.currentExperience;

    }
}
   
