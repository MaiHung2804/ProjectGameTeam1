using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class healthHandle : MonoBehaviour
{
    public Health healthSO;
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


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


        }
    }
}
