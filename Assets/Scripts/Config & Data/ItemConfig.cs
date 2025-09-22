using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    CONSUMABLE
}

[CreateAssetMenu(fileName = "ItemConfig", menuName = "ScriptableObjects/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public ItemType Type; // Phân loại vũ khí hay vật phẩm tiêu hao  
    public string id; // Id của vật phẩm
    public string itemName; // Tên vật phẩm
    public string description; // Mô tả nếu có
    public Sprite sprite; // Hình ảnh đại diện vật phẩm
   
}
