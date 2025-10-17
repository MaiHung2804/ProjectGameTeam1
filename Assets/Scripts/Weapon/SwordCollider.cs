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

    private Dictionary<int, float> hittedEnemyList;
    private const float DAMAGE_DIVISOR = 1.5f;
    private const float DAMAGE_TIMER = 1f; // seconds


    public void InitWeaponCollider(int damage)
    {
        hittedEnemyList = new Dictionary<int, float>();
        
        weaponDamage = (int) (damage / DAMAGE_DIVISOR);
        weaponCollider = GetComponent<Collider>();
        weaponCollider.isTrigger = true;

        weaponCollider.enabled = true;
    }

    //public void StartAttack()
    //{
    //    weaponCollider.enabled = true;
    //}
    //public void EndAttack()
    //{
    //    weaponCollider.enabled = false;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // LUC DAU DUNG TRUC TIEP GAMEOBJECT HOAC UNIT BASE THI BI DOI INSTANCE LIEN TUC NEN PHAI 
            // DUNG REALTIME ID DE LUU TRONG DICTIONARY
            // CO VE NHU LISTED ENEMY BI THAY DOI LIEN TUC
            EnemyBase enemyBase = other.GetComponentInParent<EnemyBase>();
            
            Debug.Log("hit Enemy" + other.gameObject.GetInstanceID());
            EnemyData enemyData = enemyBase.GetUnitData();
            int enemyId = enemyData.RunTimeId;

            //PrintListedEne(" At OnTriggerEnter");

            if (!hittedEnemyList.ContainsKey(enemyId))
            {
                enemyBase.OnTakeDamage(weaponDamage);
                hittedEnemyList[enemyId] = Time.time;
                //Debug.Log("Enemy Id: " + enemyId + " take dame " + weaponDamage + " at " + hittedEnemyList[enemyId]);

                //PrintListedEne(" At OnTriggerEnter Checked");
            }
            else if (Time.time - hittedEnemyList[enemyId] >= DAMAGE_TIMER)
            {
                enemyBase.OnTakeDamage(weaponDamage);
                hittedEnemyList[enemyId] = Time.time;
                //Debug.Log("Enemy Id: " + enemyId + " take dame after " + weaponDamage + " at " + hittedEnemyList[enemyId]);

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            //PrintListedEne(" At OnTriggerExit");

            if (enemyBase.IsDead)
            {
                Debug.Log("Enemy Id: " + enemyBase.GetUnitData().RunTimeId + " is dead, remove from hitted list.");
                EnemyData enemyData = enemyBase.GetUnitData();
                int enemyId = enemyData.RunTimeId;
                hittedEnemyList.Remove(enemyId);

            }
        }
    }

}
