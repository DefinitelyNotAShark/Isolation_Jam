using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Move c_Move;
    Attack c_Attack;

    Player player;

    private void Start()
    {
        c_Move = new Move();
        c_Attack = new Attack();

        player = GetComponent<Player>();

        c_Move.Player = player;
        c_Attack.Player = player;

        SetCommandNames();
    }

    private void Update()
    {
        Move();

        if (Input.GetButtonDown(c_Attack.Name))
        {
            c_Attack.Execute(player);
        }
    }

    /// <summary>
    /// //calls the player's move function and passes in the input values
    /// </summary>
    private void Move()
    {
        c_Move.Execute(player, Input.GetAxis(c_Move.XName), Input.GetAxis(c_Move.YName));
    }

    void SetCommandNames()
    {
        c_Move.XName = "Horizontal";
        c_Move.YName = "Vertical";
        c_Attack.Name = "Attack";
    }
}
