using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class basicSwordColliderHandle : MonoBehaviour
{

    public BaseItem equippedWeapon;   // Tham chiếu script BasicSword / Melee
    public UnitBase wielder;          // Ai đang cầm kiếm

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    //equippedWeapon.Use(target);
        //    Debug.Log("1");

        //}
    }
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("hit somethings");
        // Lấy UnitBase của kẻ địch
        
        UnitBase target = other.gameObject.GetComponent<UnitBase>();
        if (target == null) return;

        // Kiểm tra TeamID 
        if (target.TeamID != 2)
        {
            Debug.Log("target  có ID khác 2 (phải = 2 thì mới trừ máu)");
            return;
        }

        // Gọi Use() của vũ khí, truyền target
        if (equippedWeapon != null && target !=null)
        {
            equippedWeapon.Use(target);
            Debug.Log("đã gọi Hàm Use để truyền");

        }

    }
}


