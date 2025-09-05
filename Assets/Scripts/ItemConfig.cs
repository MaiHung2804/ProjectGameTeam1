using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    
}
[CreateAssetMenu(fileName = "ItemConfig", menuName = "ScriptableObjects/ItemConfig", order = 1)]
public class ItemConfig : ScriptableObject
{
    public ItemType Type; // Phân loại vũ khí hay vật phẩm tiêu hao
    public string ID; // Id của vật phẩm
    public string ItemName; // Tên vật phẩm
    public string Description; // Mô tả nếu có
    public float AttackDamage; // Sát thương của vũ khí 
    public float AttackSpeed; // Tốc độ đánh của vũ khí
    public float AttackRange; // Tầm đánh của vũ khí
    public int CurrentConsume; // Số lần sử dụng trang bị


}
