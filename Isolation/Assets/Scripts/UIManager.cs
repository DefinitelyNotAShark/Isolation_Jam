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

    private void Update()
    {
        SetUI();
    }

    void SetUI()
    {
        healthBar.fillAmount = stats.Health * .01f;
        energyBar.fillAmount = stats.Energy * .01f;
    }
}
