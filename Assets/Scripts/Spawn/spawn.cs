using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject cube;
    public Transform[] SpawnEnermy;
    public GameObject[] enermy;
    
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Spawnpoint();
            yield return new WaitForSeconds(3f);
        }
    }
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    
    void Update()
    {
        
    }

    void Spawnpoint()
    {
        int index = Random.Range(0, SpawnEnermy.Length);
        Instantiate(cube, SpawnEnermy[index].position, Quaternion.identity);

    }

    void SpawnRandomEnermy()
    {
        int prefab = Random.Range(0, enermy.Length);
        int point  = Random.Range(0, SpawnEnermy.Length);
        Instantiate(enermy[prefab], SpawnEnermy[point].position, Quaternion.identity);

    }
}
