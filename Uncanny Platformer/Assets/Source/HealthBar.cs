using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    [SerializeField] private Image totalHealthBar;

    [SerializeField] private Image currentHealthBar;
    void Start()
    {
        totalHealthBar.fillAmount = playerHealth.CurrentHealth / 100;
    }
    
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / 100;
    }
}
