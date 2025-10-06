using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyUnit : UnitBase
{
    [Header("Test Settings")]
    [SerializeField] private float damageOnCollision = 20f;

    protected override void Awake()
    {
        base.Awake();

        // Kiểm tra nếu chưa có HealthComponent thì gắn vào
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<HealthComponent>();
        }
        else
        {
            Debug.Log("chưa gắn healthComponent");
        }
               
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
        
        }
    }
}
