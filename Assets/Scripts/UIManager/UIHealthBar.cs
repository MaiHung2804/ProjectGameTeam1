using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    // Start is called before the first frame update

    void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }
    public void UpdateHealth(float current, float max)
    {
        healthSlider.value = current / max;
    }
}
