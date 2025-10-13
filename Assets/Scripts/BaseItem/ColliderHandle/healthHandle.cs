using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class healthHandle : MonoBehaviour
{

    public Consumable consumable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (consumable != null)
            {
                //consumable.Use(gameObject);
            }
            Destroy(gameObject);
        }


        
    }
}
