using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public float MaxHealth { get; private set; }
    public float MaxEnergy { get; private set; }
    public float MaxPower { get; private set; }

    public float Power { get; private set; }
    public float Health { get; private set; }
    public float Energy { get; private set; }

    public Stats(float startingHealth, float startingEnergy, float startingPower)
    {
        MaxHealth = startingHealth;//set starting stats
        MaxEnergy = startingEnergy;
        MaxPower = startingPower;

        ResetValues();
    }

    private void ResetValues()
    {
        Health = MaxHealth;
        Energy = MaxEnergy;
        Power = MaxPower;
    }

    public void DecreaseHealth(float amount)
    {
        Health -= amount;

        if (Health < 0)
            Health = 0;
    }

    public void AddHealth(float amount)
    {
        Health += amount;

        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public void DecreaseEnergy(float amount)
    {
        Energy -= amount;

        if (Energy < 0)
            Energy = 0;
    }

    public void AddEnergy(float amount)
    {
        Energy += amount;

        if (Energy > MaxEnergy)
            Energy = MaxEnergy;
    }
}
