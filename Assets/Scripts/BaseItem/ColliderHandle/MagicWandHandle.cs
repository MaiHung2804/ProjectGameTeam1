using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MagicWandHandle : MonoBehaviour
{
    public MagicWand magicWand;
    public BaseItem ItemSO;
    public GameObject fireBallPrefab;
    public Transform shottingPoint;
    public float maxDistance = 100f;
    public float fireBallSpeed = 200f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ShootFireball();
            //maxDistance = ItemSO.attackRange
            
        }
    }
    void ShootFireball()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            targetPoint = hit.point;
            //Debug.Log("Raycast đã chạm");

        }
        else
        {
            targetPoint = ray.GetPoint(maxDistance);
        }

        Vector3 direction = (targetPoint - shottingPoint.position).normalized;
        float distance = Vector3.Distance(shottingPoint.position, targetPoint);

        GameObject fireball = Instantiate(fireBallPrefab, shottingPoint.position, Quaternion.identity);
        fireball.GetComponent<fireBallHandle>().Init(distance,fireBallSpeed,direction);
        
    }
}
