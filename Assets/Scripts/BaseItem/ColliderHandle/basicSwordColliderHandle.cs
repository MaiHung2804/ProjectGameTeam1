using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSwordColliderHandle : MonoBehaviour
{
   
        public MeleeWeapon weaponData;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (weaponData != null)
                {
                    GameObject enemyObject = other.gameObject;
                    //weaponData.Use(enemyObject);
                }

            }
        }
    }


