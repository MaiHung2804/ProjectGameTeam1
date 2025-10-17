using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Item;
    public Transform[] spawnItems;
    //public GameObject[] SpItem;
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnItem();
            yield return new WaitForSeconds(2f);
        }
    }
    void Start()
    {
        //SpawnRandom();
        StartCoroutine(SpawnLoop());
        
    }

    // Update is called once per frame
    
    void Update()
    {
        

    }

    void SpawnItem()
    {
        int index = Random.Range(0, spawnItems.Length);
        Instantiate(Item, spawnItems[index].position, Quaternion.identity);

    }
    //void SpawnRandom()
    //{
        //int index = Random.Range(0, SpItem.Length);
       // int pointIndex = Random.Range(0, spawnItems.Length);
       // Instantiate(SpItem[index], spawnItems[pointIndex].position, Quaternion.identity);
    //}

}
