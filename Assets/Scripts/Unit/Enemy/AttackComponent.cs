using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private float attackDamage = 10f;
    public float AttackDamage => attackDamage;

    public void Attack(UnitBase target)
    {
        if (target != null && !target.IsDead)
        {
            Debug.Log($"{gameObject.name} tấn công {target.name}, gây {attackDamage} damage");
            target.OnTakeDamage(attackDamage);
        }
    }
}
