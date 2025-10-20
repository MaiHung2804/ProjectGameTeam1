using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIHealthBarManager : MonoBehaviour
{
    private static UIHealthBarManager instance;
    public static UIHealthBarManager Instance => instance;
    [SerializeField] private Transform healBarCanvas;
    [SerializeField] GameObject healthBarEnemyPrefab;
    private Dictionary<int, HealthBarEnemy> healthBarEnemiesDict = new Dictionary<int, HealthBarEnemy>();

    private void Awake()
    {
       instance = this;
       DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        HealthBarPlayer.Instance.Init();

        //UnitBase e1 = EnemyManager.Instance.EnemyDict[0];
        //HealthBarEnemy.Instance.UpdateInformation(0, e1.GetUnitData().Hp, e1.GetUnitData().MaxHp);
    }

    public void UpdatePlayerData()
    {
        HealthBarPlayer.Instance.UpdateInformation();
    }

    public void CreateHealthBarEnemy(int enemyRealTimeId)
    {
        if (healthBarEnemiesDict.ContainsKey(enemyRealTimeId)) { return; }        
        var hb = Instantiate(healthBarEnemyPrefab, healBarCanvas).GetComponent<HealthBarEnemy>();
        healthBarEnemiesDict.Add(enemyRealTimeId, hb);
        hb.Init(enemyRealTimeId);
    }

    public void UpdateHealthBar(int enemyRealTimeId)
    {
        if (!EnemyManager.Instance.EnemyDict.TryGetValue(enemyRealTimeId, out var enemy)) return;
        if (enemy.IsDead) return;

        HealthBarEnemy hbE = healthBarEnemiesDict[enemyRealTimeId];
        hbE.UpdateHp();
    }

    public void RemoveHealthBarEnemy(int enemyRealTimeId)
    {
        if (healthBarEnemiesDict.TryGetValue(enemyRealTimeId, out var healthBar))
        {
            //healthBar.gameObject.SetActive(false);
            Destroy(healthBar.gameObject);
            healthBarEnemiesDict.Remove(enemyRealTimeId);
        }
    }

}
