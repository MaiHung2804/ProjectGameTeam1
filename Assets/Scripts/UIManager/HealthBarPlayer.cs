using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarPlayer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image fillHbBar;
    [SerializeField] private UnityEngine.UI.Image fillManaBar;
    [SerializeField] private UnityEngine.UI.Image fillExpBar;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI hbText;
    [SerializeField] private TextMeshProUGUI manaText;

    private static HealthBarPlayer instance;
    public static HealthBarPlayer Instance => instance;

    private PlayerData playerData;

    void Awake()
    {
       instance = this;
       //DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        PlayerBase unitBase = (PlayerBase) PlayerManager.Instance.PlayerBase;
        playerData = unitBase.GetUnitData();
        UpdateInformation();
    }

    public void UpdateInformation()
    {
        UpdateHealthInformation();
        UpdateOtherInformation();
        UpdateLevelAndExp();
    }

    private void UpdateHealthInformation()
    {
        if (playerData.Hp <= 0)
        {
            fillHbBar.fillAmount = 0;
            return;
        }
        fillHbBar.fillAmount = (float)playerData.Hp / playerData.MaxHp;
        hbText.text = playerData.Hp.ToString() + "/" + playerData.MaxHp.ToString();
    }

    private void UpdateOtherInformation()
    {
        if (playerData.Mana <= 0)
        {
            fillManaBar.fillAmount = 0;
            return;
        }
        fillExpBar.fillAmount = (float)playerData.Mana / playerData.MaxMana;
        // Display Name and Gold : Temporaire
        manaText.text = "Maria Gold: " + playerData.Gold.ToString();
    }

    private void UpdateLevelAndExp()
    {
        if (playerData.CurrentExp <= 0)
        {
            fillExpBar.fillAmount = 0;
            return;
        }
        fillExpBar.fillAmount = (float)playerData.CurrentExp / playerData.MaxExp;
        level.text = playerData.Level.ToString();
    }

}
