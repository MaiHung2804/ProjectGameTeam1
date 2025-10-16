using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum EnemyType
//{
//    Melee,
//    Ranged
//}

[CreateAssetMenu(fileName = "UnitConfig", menuName = "ScriptableObjects/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public string Id; // Id cua don vi Khong duoc trung nhau
    public Team Team; // Phan loai dong minh hay ke thu

    public Skill UnitType; // Phan loai theo ky nang. Thay cho Enemy Type truoc day
    //public EnemyType type; // Phan loai danh xa hay can chien
   
    public string Name; // Ten don vi
    public string Description; // Mo ta neu co
    public Sprite Photo; // Hinh anh dai dien don vi

    public int BaseLevel;
    public int BaseHp;
    public int BaseDamage;
    public float BaseDefense;
    public int BaseMana;
    public float BaseMaxSpeed;
    public int ExpReward;
    public int MaxGoldReward;
    public int BaseGold;
    public float BaseAttackRange;

}
