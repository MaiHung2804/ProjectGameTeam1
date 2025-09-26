using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Melee/basicSword")]
public class basicSword : MeleeWeapon
{
    
    public override void Use(UnitBase target)
    {
        Debug.Log(target);
        Debug.Log("in BasicSword");
        Debug.Log(target);
        target.OnTakeDamage(damage);
    }
}