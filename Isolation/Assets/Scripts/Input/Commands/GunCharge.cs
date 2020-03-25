using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCharge : PlayerCommand
{

    public override void Execute(Player player)
    {
        player.GunCharge();
    }

    public override void Execute(Player player, float x, float y)
    {
        player.GunCharge(x, y);
    }
}
