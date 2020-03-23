using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //PlayerCommands
    Move pc_Move;
    Attack pc_Attack;

    //ButtonCommands
    ToggleDebug bc_Toggle;

    Player player;
    IButtonListener panelListener;

    private void Start()
    {
        pc_Move = new Move();
        pc_Attack = new Attack();
        bc_Toggle = new ToggleDebug();

        player = GetComponent<Player>();

        panelListener = GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<IButtonListener>();//find the listener on the debug panel

        pc_Move.Player = player;
        pc_Attack.Player = player;
        bc_Toggle.Listener = panelListener;

        SetCommandNames();
    }

    private void Update()
    {
        Move();
        Attack();
        ToggleDebugPanel();
    }

    private void SetCommandNames()
    {
        pc_Move.XName = "Horizontal";
        pc_Move.YName = "Vertical";
        pc_Attack.Name = "Attack";
        bc_Toggle.Name = "Toggle";
    }

    private void ToggleDebugPanel()
    {
        if (Input.GetButtonDown(bc_Toggle.Name))
        {
            bc_Toggle.Execute();
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown(pc_Attack.Name))
        {
            pc_Attack.Execute(player);
        }
    }

    /// <summary>
    /// //calls the player's move function and passes in the input values
    /// </summary>
    private void Move()
    {
        pc_Move.Execute(player, Input.GetAxis(pc_Move.XName), Input.GetAxis(pc_Move.YName));
    }

}
