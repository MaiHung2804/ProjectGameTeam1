using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThiefConfig", menuName = "ScriptableObjects/ThiefConfig", order = 2)]
public class ThiefConfig : ScriptableObject
{
    public UnitConfig UnitConfig; // Thông tin cấu hình của đơn vị
    public GameObject ThiefConfigPrefab;
    public Vector3[] SpawnPoints; // Mảng các điểm spawn
    public Vector3[] PatrolPoints; // Mảng các điểm tuần tra
}
