using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar, energyBar;
    [SerializeField] private float maxHealth = 100, maxEnergy = 100;

    private float healthAmount, energyAmount;

    void Start()
    {
        healthAmount = maxHealth;
        energyAmount = maxEnergy;

        SetHealth();
        SetEnergy();

    }

    private void SetHealth()
    { 
        healthBar.fillAmount = healthAmount * .01f;
    }

    private void SetEnergy()
    {
        energyBar.fillAmount = energyAmount * .01f;
    }

    public void DecreaseHealth(float amount)
    {
        healthAmount -= amount;

        if (healthAmount < 0)
            healthAmount = 0;

        SetHealth();
    }

    public void AddHealth(float amount)
    {
        healthAmount += amount;

        if (healthAmount > maxHealth)
            healthAmount = maxHealth;

        SetHealth();
    }

    public void DecreaseEnergy(float amount)
    {
        energyAmount -= amount;

        if (energyAmount < 0)
            energyAmount = 0;

        SetEnergy();
    }

    public void AddEnergy(float amount)
    {
        energyAmount += amount;

        if (energyAmount > maxEnergy)
            energyAmount = maxEnergy;

        SetEnergy();
    }
}
