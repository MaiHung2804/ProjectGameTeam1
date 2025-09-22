using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Melee,
    Ranged
}
[CreateAssetMenu(fileName = "UnitConfig", menuName = "ScriptableObjects/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public EnemyType type; // Phân loại đánh xa hay cận chiến //
    public string id; // Id của đơn vị
    public string enemyName; // Tên đơn vị
    public string description; // Mô tả nếu có
    public Sprite sprite; // Hình ảnh đại diện đơn vị


}
