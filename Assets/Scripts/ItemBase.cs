using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public enum ItemType
{
    MeleeWeapon,
    RangedWeapon,
    Consumable
}

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

    public abstract void Use(GameObject user);
}

