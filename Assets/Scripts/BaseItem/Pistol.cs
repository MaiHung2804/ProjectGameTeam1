using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/RangedWeapon/Pistol")]
public class Pistol : RangedWeapon
{
    public override void Use(GameObject user)
    {
        throw new System.NotImplementedException();
    }
}