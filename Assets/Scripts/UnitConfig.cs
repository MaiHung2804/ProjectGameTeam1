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
    public EnemyType Type; // Phân loại đánh xa hay cận chiến //
    public string ID; // Id của đơn vị
    public string EnemyName; // Tên đơn vị
    public string Description; // Mô tả nếu có
    public float Health; // Hp 
    public float AttackBase; // Sát thương
    public float AttackSpeedBase; // Tốc độ đánh
    public float AttackRangeBase; // Tầm đánh
    public float MoveSpeedBase; // Tốc độ di chuyển
    public float CooldownBase; // Thời gian hồi chiêu
    
}
