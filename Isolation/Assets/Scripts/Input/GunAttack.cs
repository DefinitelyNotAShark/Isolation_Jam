using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttack : PlayerCommand
{
    public Player Player;

    public override void Execute(Player player)
    {
        player.GunAttack();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}
