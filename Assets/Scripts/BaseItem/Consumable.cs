using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : BaseItem
{
    public int amount;

    internal void Use()
    {
        throw new NotImplementedException();
    }
}

