using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform[] SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        CharacterSpawn();
    }
    void CharacterSpawn()
    {
        int index = Random.Range(0, SpawnPoint.Length);
        Instantiate(characterPrefab, SpawnPoint[index].position, SpawnPoint[index].rotation);
    }

    // Update is called once per frame
    
    void Update()
    {
        
    }
}
