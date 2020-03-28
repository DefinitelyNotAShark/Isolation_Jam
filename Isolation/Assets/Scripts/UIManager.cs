using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image healthBar, energyBar;

    private Stats stats;

    void Start()
    {
        stats = player.Stats;
        SetUI();
    }

    void SetUI()
    {
        healthBar.fillAmount = stats.Health * .01f;
        energyBar.fillAmount = stats.Energy * .01f;
    }

    public void AddHealth(float amount)
    {
        stats.AddHealth(amount);
        SetUI();
    }

    public void DecreaseHealth(float amount)
    {
        stats.DecreaseHealth(amount);
        SetUI();
    }

    public void AddEnergy(float amount)
    {
        stats.AddEnergy(amount);
        SetUI();
    }

    public void DecreaseEnergy(float amount)
    {
        stats.DecreaseEnergy(amount);
        SetUI();
    }
}
