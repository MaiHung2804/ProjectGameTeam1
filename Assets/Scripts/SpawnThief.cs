using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThief : MonoBehaviour
{
    public ThiefConfig thiefConfig; // Thông tin cấu hình của kẻ trộm

    void Start()
    {
        GameObject gameObject = Instantiate(thiefConfig.ThiefConfigPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
