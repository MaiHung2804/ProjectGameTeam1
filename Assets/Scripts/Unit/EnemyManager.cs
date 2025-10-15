using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    [SerializeField] UnitConfig enemyConfig;
    [SerializeField] GameObject enemyPrefab;
    private int enemyCount = 0;

    [SerializeField] Transform[] spawnPoints;
    private List<UnitBase> enemyList;
    public List<UnitBase> EnemyList { get { return enemyList; } private set { } }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        enemyList = new List<UnitBase>();
        enemyCount++;
        enemyList.Add( SpawnEnemy(enemyPrefab, spawnPoints[0].position, spawnPoints[1].position));
    }

    private UnitBase SpawnEnemy(GameObject enemyPrefab, Vector3 patrolA, Vector3 patrolB)
    {
        GameObject enemyObject = Instantiate(enemyPrefab, patrolA, Quaternion.identity);
        UnitBase enemyBase = enemyObject.GetComponent<UnitBase>();
        EnemyData enemyData = new EnemyData(enemyConfig, patrolA, patrolB);
        enemyBase.SetUnitData(enemyData);
        enemyBase.Init();
        return enemyBase;
    }
   


}
