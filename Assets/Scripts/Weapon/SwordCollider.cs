using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private int weaponDamage;
    public int WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    private Collider weaponCollider;
    public Collider WeaponCollider { get => weaponCollider; set => weaponCollider = value; }

    private Dictionary<int, float> hittedEnemyList;
    private const float DAMAGE_DIVISOR = 1.5f;
    private const float DAMAGE_TIMER = 1f; 


    public void InitWeaponCollider(int damage)
    {
        hittedEnemyList = new Dictionary<int, float>();
        weaponDamage = (int) (damage / DAMAGE_DIVISOR);
        weaponCollider = GetComponent<Collider>();
        weaponCollider.isTrigger = true;
        weaponCollider.enabled = true;
    }

    // KHONG BAT TAT LIEN TUC
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
        if (PlayerManager.Instance.PlayerBase.CurrentState != UnitBase.UnitState.Attack)
            return;

        if (other.CompareTag(TagName.ENEMY))
        {
            // LUC DAU DUNG TRUC TIEP GAMEOBJECT HOAC UNIT BASE THI BI DOI INSTANCE LIEN TUC NEN PHAI 
            // DUNG REALTIME ID DE LUU TRONG DICTIONARY
            // CO VE NHU LISTED ENEMY BI THAY DOI LIEN TUC
            EnemyBase enemyBase = other.GetComponentInParent<EnemyBase>();
            EnemyData enemyData = enemyBase.GetUnitData();
            int enemyId = enemyData.RunTimeId;

            if (!hittedEnemyList.ContainsKey(enemyId))
            {
                enemyBase.OnTakeDamage(weaponDamage);
                hittedEnemyList[enemyId] = Time.time;
            }
            else if (Time.time - hittedEnemyList[enemyId] >= DAMAGE_TIMER)
            {
                enemyBase.OnTakeDamage(weaponDamage);
                hittedEnemyList[enemyId] = Time.time;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName.ENEMY))
        {
            EnemyBase enemyBase = other.GetComponentInParent<EnemyBase>();
            if (enemyBase.IsDead)
            {
                EnemyData enemyData = enemyBase.GetUnitData();
                int enemyId = enemyData.RunTimeId;
                hittedEnemyList.Remove(enemyId);
            }
        }
    }

}
