using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;


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
    [SerializeField] private int enemyCount = 12;

    private const float RANDOM_RADIUS = 3f;
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
        if (enemyCount < 10)
        {
            enemyCount = 10;
        }
        SpawnAllEnemies(enemyCount);
    }

    private void SpawnAllEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pA;
            Vector3 pB;
            if (GetRandomPatrolPoint(out pA, out pB))
            {
                enemyDict.Add(i, SpawnEnemy(enemyPrefab, pA, pB,i));
            }
            else
            {
                Debug.Log("Khong The Sinh Duoc");
            }

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

    public bool GetRandomPatrolPoint(out Vector3 patrolA, out Vector3 patrolB)
    {
        if (spawnPoints.Length == 0)
        {
            patrolA = Vector3.zero;
            patrolB = Vector3.zero;
            return false;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 baseA = spawnPoints[randomIndex].pointA.position;
        Vector3 baseB = spawnPoints[randomIndex].pointB.position;

        Vector3 ranPointA = baseA;
        Vector3 ranPointB = baseB;
        bool found = false;
        for (int i = 0; i < 100; i ++)
        {
            ranPointA = baseA + Random.insideUnitSphere * RANDOM_RADIUS;
            ranPointB = baseB + Random.insideUnitSphere * RANDOM_RADIUS;
            ranPointA.y = baseA.y;
            ranPointB.y = baseB.y;
            NavMeshHit hitA;
            NavMeshHit hitB;
            if (NavMesh.SamplePosition(ranPointA, out hitA, 1.0f, NavMesh.AllAreas))
            {
                ranPointA = hitA.position;
            }
            if (NavMesh.SamplePosition(ranPointB, out hitB, 1.0f, NavMesh.AllAreas))
            {
                ranPointB = hitB.position;
            }
            NavMeshPath path = new NavMeshPath();
            if ( (NavMesh.CalculatePath(ranPointA,ranPointB,NavMesh.AllAreas,path)
                && path.status == NavMeshPathStatus.PathComplete))
            {
                found = true;
                break;
            }
        }
        if (found)
        {
            patrolA = ranPointA;
            patrolB = ranPointB;
        }
        else
        { patrolA = baseA;
          patrolB = baseB;
        }
            return true; 
    }

}
