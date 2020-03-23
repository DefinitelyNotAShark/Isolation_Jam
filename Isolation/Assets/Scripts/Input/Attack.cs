using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Command
{
    public Player Player;

    public override void Execute(Player player)
    {
        player.Attack();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}
