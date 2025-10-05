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
        if (player == null)
        {
            player = FindObjectOfType<PlayerControllerTest>();
            if (player == null)
            {
                Debug.LogError("PlayerControllerTest not found in the scene.");
                return;
            }
        }
        StartCoroutine(DelayedUIUpdate());
    }
    private IEnumerator DelayedUIUpdate()
    {
        yield return new WaitForSeconds(0.5f); // Đợi 0.5 giây để đảm bảo PlayerControllerTest đã khởi tạo xong
        UpdateUI(); // Cập nhật giao diện người chơi sau khi đợi
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
        if (player == null || player.playerData == null)
            return;
        var data = player.playerData;
        playerNameText.text = data.userName;       
        playerLevelText.text = data.userLevel.ToString();       
        
        uiHealthBar.maxValue = data.maxHp; 
        uiHealthBar.value = data.currentHp;       
        
        uiManaBar.maxValue = data.maxMana;  
        uiManaBar.value = data.currentMana;       
        
        uiExpBar.maxValue = data.experienceToNextLevel;
        uiExpBar.value = data.currentExperience;   
        
    }
}


