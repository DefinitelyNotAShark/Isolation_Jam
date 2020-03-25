using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact: PlayerCommand
{
    public override void Execute(Player player)
    {
        player.Interact();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}
