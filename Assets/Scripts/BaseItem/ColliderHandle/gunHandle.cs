using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunHandle : MonoBehaviour
{
    [Header("Weapon Data")]
    public RangedWeapon weaponData;

    [Header("Fire Settings")]
    public Transform firePoint;

    private float fireCooldown;

    private float range;
    private float lastFireTime;
    private void Start()
    {

        range = weaponData.attackRange;
        fireCooldown = (1f / weaponData.fireRate);

        range =  weaponData.attackRange;
        fireCooldown = (1f/ weaponData.fireRate);

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastFireTime + fireCooldown)
        {
            Fire();
        }
    }

    void Fire()
    {
        lastFireTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            GameObject target = hit.collider.gameObject;

            if (target.CompareTag("Enemy"))
            {
                weaponData?.Use(target); // Gọi xử lý từ ScriptableObject
            }
            // Optional: debug line
            Debug.DrawLine(firePoint.position, hit.point, Color.red, 0.2f);
        }
    }
}
