using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum ItemType
{
    MeleeWeapon,
    RangedWeapon,
    Consumable
}
public abstract class ItemConfig : ScriptableObject
{
    public ItemType Type; // Phân loại vũ khí hay vật phẩm tiêu hao  
    public string ID; // Id của vật phẩm
    public string ItemName; // Tên vật phẩm
    public string Description; // Mô tả nếu có
    public float AttackDamage; // Sát thương của vũ khí 
    public float AttackSpeed; // Tốc độ đánh của vũ khí
    public float AttackRange; // Tầm đánh của vũ khí
    public GameObject prefab;
    public int CurrentConsume; // Số lần sử dụng trang bị
    public abstract void Use();

}
