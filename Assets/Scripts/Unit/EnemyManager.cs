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

    private Dictionary<int, UnitBase> enemyDict;
    public Dictionary<int, UnitBase> EnemyDict { get { return enemyDict; } private set { } }

    void Awake()
    {
        Instance = this;
    }
    public void Init()
    {
        enemyDict = new Dictionary<int, UnitBase>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemyCount++;
            enemyDict.Add(i,SpawnEnemy(enemyPrefab, spawnPoints[i].pointA.position, spawnPoints[i].pointB.position,i));
        }
        //Debug.Log("EnemyManager Init with " + enemyDict.Count + " enemies.");
    }

    private UnitBase SpawnEnemy(GameObject enemyPrefab, Vector3 patrolA, Vector3 patrolB, int runTimeId)
    {
        //Debug.Log("Spawn Enemy at: " + patrolA);

        GameObject enemyObject = Instantiate(enemyPrefab, patrolA, Quaternion.identity);
        UnitBase enemyBase = enemyObject.GetComponent<EnemyBase>();
        EnemyData enemyData = new EnemyData(ConfigManager.Instance.GetUnitConfig("e1"), patrolA, patrolB, runTimeId);
        enemyBase.SetUnitData(enemyData);
        enemyBase.Init();
        return enemyBase;
    }
   
    public EnemyData GetEnemyData(int runTimeId)
    {
        return (EnemyData)enemyDict[runTimeId].GetUnitData();
    }

    public void RemoveEnemy(int runTimeId)
    {
        if (enemyDict.ContainsKey(runTimeId))
        {
            enemyDict.Remove(runTimeId);
            //Debug.Log("Enemy with ID " + runTimeId + " removed from EnemyManager.");
        }
    }

}
