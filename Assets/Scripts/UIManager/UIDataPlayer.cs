using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDataPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private Slider uiHealthBar;
    [SerializeField] private Slider uiManaBar;
    [SerializeField] private Slider uiExpBar;
    private PlayerControllerTest player;

    private void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<PlayerControllerTest>();
        }
        UpdateUI();
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateUI();
        }
    }

    public void UpdateUI() // Cập nhật giao diện người chơi
    {
        var data = DataManager.Instance.player;
        if (data == null) return;

        if (playerNameText != null) 
            playerNameText.text = data.userName;

        if (playerLevelText != null)
            playerLevelText.text = data.userLevel.ToString();

        if (uiHealthBar != null)
        {
            uiHealthBar.maxValue = data.maxHp; 
            uiHealthBar.value = data.currentHp;
        }

        if (uiManaBar!= null)
        {
            uiManaBar.maxValue = data.maxMana;  
            uiManaBar.value = data.currentMana;
        }
        if (uiExpBar != null)
        {
            uiExpBar.maxValue = data.experienceToNextLevel;
            uiExpBar.value = data.currentExperience;   
        }
    }
}


