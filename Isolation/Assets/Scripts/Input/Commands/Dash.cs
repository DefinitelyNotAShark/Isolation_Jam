using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerCommand
{

    public override void Execute(Player player)
    {
        player.Dash();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}
