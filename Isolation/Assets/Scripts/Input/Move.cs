using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Command
{
    public string XName, YName;
    public Player Player;

    public override void Execute(Player player, float xAmount, float yAmount)
    {
        player.Move(xAmount, yAmount);
    }

    public override void Execute(Player player)//Not needed
    {
        throw new System.NotImplementedException();
    }
}
