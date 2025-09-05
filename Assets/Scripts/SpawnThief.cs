using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThief : MonoBehaviour
{
    public UnitConfig unitConfig;
    public ItemConfig itemConfig;
    void Start()
    {
        Instantiate(itemConfig.ItemPrefabs, transform.position, Quaternion.identity);
        Instantiate(unitConfig.UnitPrefabs, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
