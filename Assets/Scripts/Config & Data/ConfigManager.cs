using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance { get; private set; }

    [SerializeField] private List<ItemConfig> itemConfig;
    [SerializeField] private List<UnitConfig> unitConfig;

    private Dictionary<string, ItemConfig> configDict;
    private Dictionary<string, UnitConfig> unitDict;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void InitItemDictionary()
    {
        configDict = new Dictionary<string, ItemConfig>();
        foreach (var config in itemConfig)
        {
            if (!configDict.ContainsKey(config.id))
            {
                configDict.Add(config.id, config);
            }
            else
            {
                Debug.LogWarning($"Duplicate ItemConfig ID detected: {config.id}");
            }
        }
    
    }

    public void InitUnitDictionary()
    {
        unitDict = new Dictionary<string, UnitConfig>();
        foreach (var config in unitConfig)
        {
            if (!unitDict.ContainsKey(config.id))
            {
                unitDict.Add(config.id, config);
            }
            else
            {
                Debug.LogWarning($"Duplicate UnitConfig ID detected: {config.id}");
            }
        }
    }
    public ItemConfig GetConfig(string itemId)
    {
        if (configDict.TryGetValue(itemId, out ItemConfig config))
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
