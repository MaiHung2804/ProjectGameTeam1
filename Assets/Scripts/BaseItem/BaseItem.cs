using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum ItemType
{
    MeleeWeapon,
    RangedWeapon,
    Consumable
}

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public GameObject Prefab;

    public abstract void Use(UnitBase target);
}

