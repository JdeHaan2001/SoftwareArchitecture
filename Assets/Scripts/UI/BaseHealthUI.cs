using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHealthUI : MonoBehaviour
{
    [SerializeField]
    private BaseManager baseManager;
    [SerializeField]
    private GameObject loseText;
    private Slider slider;

    private void OnEnable()
    {
        baseManager.OnDealDamageToBase += UpdateHealthUI;

        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = baseManager.GetBaseHealth();
        slider.value = slider.maxValue;
    }

    private void OnDisable()
    {
        baseManager.OnDealDamageToBase -= UpdateHealthUI;
    }

    public void UpdateHealthUI(int pHealthLeft)
    {
        slider.value = pHealthLeft;
    }
}
