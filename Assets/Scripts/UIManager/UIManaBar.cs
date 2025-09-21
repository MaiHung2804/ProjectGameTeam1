using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManaBar : MonoBehaviour
{
    [SerializeField] private Slider manaSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        manaSlider = GetComponent<Slider>();
    }
    public void UpdateMana(float current, float max)
    {
        manaSlider.value = current / max;
    }
}
