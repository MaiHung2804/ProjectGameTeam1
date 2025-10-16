using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    private UnitBase playerBase;
    public UnitBase PlayerBase { get { return playerBase; } private set { } }

    private GameObject playerObject;
    [SerializeField] GameObject playerPrefab;
    private Vector3 playerTransform = new Vector3(-3, 5, 12);

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        playerObject = Instantiate(playerPrefab, playerTransform, Quaternion.identity);
        playerBase = playerObject.GetComponent<UnitBase>();
        // LAY DU LIEU TU CONFIG
        PlayerData playerData = new PlayerData( ConfigManager.Instance.GetUnitConfig("p1"));
        playerBase.SetUnitData(playerData);
        playerBase.Init();

    }

    public UnitBase SelectedPlayerTarget()
    {
        if ( (playerObject == null) || (playerBase.IsDead) )
        {  return null; }
        return playerBase;
    }    

}
