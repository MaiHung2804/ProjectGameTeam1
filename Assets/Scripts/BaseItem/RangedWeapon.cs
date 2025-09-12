
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RangedWeapon : ItemConfig
{
    public float attackRange;

    internal void Use(GameObject target)
    {
        throw new NotImplementedException();
    }
}
