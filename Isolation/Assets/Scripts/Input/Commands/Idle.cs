using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle: PlayerCommand
{
    public override void Execute(Player player)
    {
        player.Idle();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}
