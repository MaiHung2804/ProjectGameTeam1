using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public UnitBase Player { get; private set; }


    private GameObject playerObject;
    [SerializeField] GameObject playerPrefab;
    private Vector3 playerTransform = new Vector3(-3, 5, 12);
    [SerializeField] UnitConfig playerConfig;


    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        playerObject = Instantiate(playerPrefab, playerTransform, Quaternion.identity);
        Player = playerObject.GetComponent<UnitBase>();
        PlayerData playerData = new PlayerData(playerConfig);
        Player.SetUnitData(playerData);
        Player.Init();

    }

    public Transform SelectedPlayerTarget()
    {
        if ( (playerObject == null) || (Player.IsDead) )
        {  return null; }
        return playerObject.transform;
    }    

}
