using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIPlayer : MonoBehaviour
{
    public TextMeshProUGUI playerName;

    void Start()
    {
        playerName.text = DataManager.Instance.Username;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
