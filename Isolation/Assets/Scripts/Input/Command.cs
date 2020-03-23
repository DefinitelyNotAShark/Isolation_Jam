using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public string Name;
    public abstract void Execute(Player player);
    public abstract void Execute(Player player, float x, float y);
}
