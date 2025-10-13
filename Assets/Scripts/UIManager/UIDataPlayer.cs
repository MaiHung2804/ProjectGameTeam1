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



    IEnumerator Start()
    {
        yield return new WaitUntil(() => DataManager.Instance.player != null);
        player = DataManager.Instance.player;
        UppdateUI();
    }


    private void Update()
    {
        UppdateUI();
    }
   
    public void UppdateUI()
    {
        if (player == null) return;

        playerNameText.text = player.Name;
        playerLevelText.text = player.Level.ToString();
        
        uiHealthBar.maxValue = player.MaxHp;
        uiHealthBar.value = player.Hp;

        uiManaBar.maxValue = player.MaxMana;
        uiManaBar.value = player.Mana;
        
        uiExpBar.maxValue = player.MaxExp;
        uiExpBar.value = player.CurrentExp;

    }
}
   
