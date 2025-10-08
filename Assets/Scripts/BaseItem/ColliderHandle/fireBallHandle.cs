using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallHandle : MonoBehaviour
{
    public MagicWand magicWandSO;
    float lifeTime;
    Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(float distance, float Speed, Vector3 direction)
    {
        rb.velocity = direction * Speed;
         lifeTime = distance / Speed;
        Destroy(gameObject, lifeTime+2f); // trừ hao thêm 2 giây cho chắc nếu ko va chạm
        Debug.Log(direction);
        Debug.Log(Speed);
        //Debug.Log();
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null) 
        {
            UnitBase target = collision.gameObject.GetComponent<UnitBase>();
            if ((int)target.TeamID != 2 && (int)target.TeamID != 3) 
            {
                //Debug.Log("TeamID != 2 or 3 ");
                return; 
            }
            target.OnTakeDamage(50);
            //Debug.Log("Đã gọi OnTakeDamage");
        Destroy(gameObject);

        }
    }
}
