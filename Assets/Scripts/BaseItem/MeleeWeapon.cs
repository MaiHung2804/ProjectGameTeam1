using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class MeleeWeapon : ItemConfig // todo : melee weapon has a itemid
//itemid => configmanager => itemConfig
//item id=> data manager => item data
{

    public int damage;
    public float attackRate;

}
