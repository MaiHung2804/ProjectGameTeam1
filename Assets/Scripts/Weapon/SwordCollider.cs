using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    // Damage co the truyen tu UnitData, hoac WeaponData sau nay
    private int weaponDamage;
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    private Collider weaponCollider;
    public Collider WeaponCollider { get => weaponCollider; set => weaponCollider = value; }

    public void InitWeaponCollider(int damage)
    {
        weaponDamage = damage;
        weaponCollider = GetComponent<Collider>();
        weaponCollider.isTrigger = true;
        weaponCollider.enabled = false;
    }
   
    public void StartAttack()
    {
        weaponCollider.enabled = true;
    }
    public void EndAttack()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Giam mau enemy
            var enemyHealth = other.GetComponent<UnitBase>();
            if (enemyHealth != null)
            {
                enemyHealth.OnTakeDamage(weaponDamage);
            }
        }
    }

}
