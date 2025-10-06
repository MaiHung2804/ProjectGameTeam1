using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemConfig
{
    public int amount;


    public virtual void Use(UnitBase target) // tạm fix
    {
        throw new NotImplementedException();
    }

}

