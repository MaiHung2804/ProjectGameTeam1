using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaHandle : MonoBehaviour
{
    public Consumable consumable;
    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space))
        {
            if(consumable != null)
            {
                consumable.Use(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
