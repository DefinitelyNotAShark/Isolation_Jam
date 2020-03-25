using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : PlayerCommand
{
    public string XName, YName;
    public Player Player;
    public bool CanMove;

    public override void Execute(Player player, float xAmount, float yAmount)
    {
        player.Move(xAmount, yAmount, CanMove);
    }

    public override void Execute(Player player)//Not needed
    {
        throw new System.NotImplementedException();
    }
}
