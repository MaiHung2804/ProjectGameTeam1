using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HealthBarPlayer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image emptyBar;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI aboveText;
    [SerializeField] private TextMeshProUGUI belowText;

    private static HealthBarPlayer instance;
    public static HealthBarPlayer Instance => instance;

    private PlayerData playerData;

    void Awake()
    {
       instance = this;
       DontDestroyOnLoad(gameObject);
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
        UpdateNameAndGold();
        UpdateLevelAndExp();
    }

    private void UpdateHealthInformation()
    {
        if (playerData.Hp <= 0)
        {
            emptyBar.fillAmount = 1;
            return;
        }
        emptyBar.fillAmount = 1 - (float)playerData.Hp / playerData.MaxHp;
    }

    private void UpdateNameAndGold()
    {
        string infor = playerData.Name.ToString() + " " + playerData.Gold.ToString() + "$";
        aboveText.text = infor;
    }

    private void UpdateLevelAndExp()
    {
        //string infor = playerData.CurrentExp.ToString() + "/" + playerData.MaxExp.ToString();
        string infor = playerData.Hp.ToString () + "/" + playerData.MaxHp.ToString();

        belowText.text = infor;
        level.text = playerData.Level.ToString();
    }

}
