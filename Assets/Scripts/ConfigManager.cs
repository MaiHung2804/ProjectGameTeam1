using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance { get; private set; }
    [SerializeField] private List<ItemConfig> itemConfigs;
    private Dictionary<string, ItemConfig> configDict;
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
    public void InitDictionary()
    {
        configDict = new Dictionary<string, ItemConfig>();
        foreach (var config in itemConfigs)
        {
            if (!configDict.ContainsKey(config.ID))
                configDict.Add(config.ID, config);
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
}
