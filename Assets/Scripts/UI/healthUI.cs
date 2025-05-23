using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private HealthSystem health;

    private void Start()
    {
        health.OnHealthChanged += UpdateUI;
    }

    private void UpdateUI(float raito)
    {
        healthFill.fillAmount = raito;
    }
}
