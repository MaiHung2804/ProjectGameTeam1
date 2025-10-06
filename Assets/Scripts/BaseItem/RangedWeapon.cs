
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RangedWeapon : ItemConfig
{
    public float attackRange;
    public float fireRate;

    public virtual void Use(UnitBase target) // tạm fix
    {
        Debug.Log("in RangedWeapon");
        target.OnTakeDamage(10);
    }

}
