using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class PatrolPoint
{
    public Transform pointA;
    public Transform pointB;
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    [SerializeField] GameObject enemyPrefab;
    private int enemyCount = 0;

    [SerializeField] PatrolPoint[] spawnPoints;

    private List<UnitBase> enemyList;
    public List<UnitBase> EnemyList { get { return enemyList; } private set { } }

    void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
        enemyList = new List<UnitBase>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemyCount++;
            enemyList.Add(SpawnEnemy(enemyPrefab, spawnPoints[i].pointA.position, spawnPoints[i].pointB.position));
        }
    }

    private UnitBase SpawnEnemy(GameObject enemyPrefab, Vector3 patrolA, Vector3 patrolB)
    {
        Debug.Log("Spawn Enemy at: " + patrolA);

        GameObject enemyObject = Instantiate(enemyPrefab, patrolA, Quaternion.identity);
        UnitBase enemyBase = enemyObject.GetComponent<EnemyBase>();
        EnemyData enemyData = new EnemyData(ConfigManager.Instance.GetUnitConfig("e1"), patrolA, patrolB);
        enemyBase.SetUnitData(enemyData);
        enemyBase.Init();
        return enemyBase;
    }
   


}
