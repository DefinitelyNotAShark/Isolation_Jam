﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : PlayerCommand
{
    public Player Player;

    public override void Execute(Player player)
    {
        player.SwordAttack();
    }

    public override void Execute(Player player, float x, float y)
    {
        throw new System.NotImplementedException();
    }
}