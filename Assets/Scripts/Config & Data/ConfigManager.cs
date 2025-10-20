using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance {  get; private set; }
    [SerializeField] private List<ItemConfig> itemConfigList;
    [SerializeField] private List<UnitConfig> unitConfigList;
    private Dictionary<string, ItemConfig> itemDict;
    private Dictionary<string, UnitConfig> unitDict;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        InitItemDictionary();
        InitUnitDictionary();
    }

    public void InitItemDictionary()
    {
        if  ( (itemConfigList == null) || (itemConfigList.Count == 0))
        {
            itemDict = null;
            return;
        }
        itemDict = new Dictionary<string, ItemConfig>();
        foreach (ItemConfig config in itemConfigList)
        {
            if (!itemDict.ContainsKey(config.id))
            {
                itemDict.Add(config.id, config);
            }
            else
            {
                Debug.LogWarning($"Duplicate ItemConfig ID detected: {config.id}");
            }
        }
    }

    public void InitUnitDictionary()
    {
        if ( (unitConfigList == null) || (unitConfigList.Count == 0))
        {
            unitDict = null;
            return;
        }
        unitDict = new Dictionary<string, UnitConfig>();
        foreach (var config in unitConfigList)
        {
            if (!unitDict.ContainsKey(config.Id))
            {
                unitDict.Add(config.Id, config);
            }
            else
            {
                Debug.LogWarning($"Duplicate UnitConfig ID detected: {config.Id}");
            }
        }
    }
    public ItemConfig GetItemConfig(string itemId)
    {
        if (itemDict.TryGetValue(itemId, out ItemConfig config))
        {
            return config;
        }
        Debug.LogWarning($"ItemConfig with ID {itemId} not found!");
        return null;
    }

    public UnitConfig GetUnitConfig(string unitId)
    {
        if (unitDict.TryGetValue(unitId, out UnitConfig config))
        {
            return config;
        }
        Debug.LogWarning($"UnitConfig with ID {unitId} not found!");
        return null;
    }

}
