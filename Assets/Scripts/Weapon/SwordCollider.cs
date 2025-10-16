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

    //private class HitInfo
    //{
    //    public int tickCount = 0;
    //    public float lasHitTime = 0f;
    //}

    private Dictionary<GameObject, float> enemiesHit = new();
    private const int DAMAGE_TICK_COUNT = 5;
    private const float DAMAGE_TICK_INTERVAL = 4f; // seconds


    public void InitWeaponCollider(int damage)
    {
        weaponDamage = damage / DAMAGE_TICK_COUNT;
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
        enemiesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // LUC DAU DUNG UNITBASE BI DOI KEY LIEN TUC
            var enemyObject = other.gameObject;
            var enemyBase = other.GetComponent<UnitBase>();
            if (!enemiesHit.ContainsKey(enemyObject))
            {
                enemyBase.OnTakeDamage(weaponDamage);
                Debug.Log("Sword damage: " + other.name + " dame " + weaponDamage);
                enemiesHit[enemyObject] = Time.time;
                Debug.Log(enemyBase.name + " time hit: " + enemiesHit[enemyObject]);
            }
            //else if (Time.time - enemiesHit[enemyBase] >= DAMAGE_TICK_INTERVAL)
            //{
            //    enemyBase.OnTakeDamage(weaponDamage);
            //    Debug.Log("Sword damage: " + other.name + " dame " + weaponDamage);
            //    enemiesHit[enemyBase] = Time.time;
            //}

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyObject = other.gameObject;
            Debug.Log("Sword end: " + other.name);
            var enemyBase = other.GetComponent<UnitBase>();
            if (enemiesHit.ContainsKey(enemyObject) && (enemyBase.IsDead))
            {
                enemiesHit.Remove(enemyObject);
            }
        }
    }


}
