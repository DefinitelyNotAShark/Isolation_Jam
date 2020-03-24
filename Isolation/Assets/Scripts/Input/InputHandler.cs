using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //PlayerCommands
    Move pc_Move;
    GunAttack pc_GunAttack;
    SwordAttack pc_SwordAttack;
    Dash pc_Dash;

    //ButtonCommands
    ToggleDebug bc_Toggle;

    Player player;
    IButtonListener panelListener;

   private Animator anim;
    private bool shooting = false;

    private void Start()
    {
        pc_Move = new Move();
        pc_GunAttack = new GunAttack();
        pc_SwordAttack = new SwordAttack();
        bc_Toggle = new ToggleDebug();
        pc_Dash = new Dash();

        player = GetComponent<Player>();
        anim = player.GetComponent<Animator>();

        panelListener = GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<IButtonListener>();//find the listener on the debug panel

        //set the player
        pc_Move.Player = player;
        pc_GunAttack.Player = player;
        pc_SwordAttack.Player = player;
        pc_Dash.Player = player;

        bc_Toggle.Listener = panelListener;

        SetCommandNames();
    }

    private void Update()
    {
        Move();
        Dash();
        GunAttack();
        SwordAttack();
        ToggleDebugPanel();
    }

    private void SetCommandNames()
    {
        pc_Move.XName = "Horizontal";
        pc_Move.YName = "Vertical";
        pc_Dash.Name = "Dash";
        pc_SwordAttack.Name = "Slash";
        pc_GunAttack.Name = "Shoot";
        bc_Toggle.Name = "Toggle";
    }

    private void ToggleDebugPanel()
    {
        if (Input.GetButtonDown(bc_Toggle.Name))
        {
            bc_Toggle.Execute();
        }
    }

    private void GunAttack()
    {
        if (Input.GetButton(pc_GunAttack.Name))
        {
            if (!shooting)
            {
                shooting = true;//button is pressed
                //AUDIO PLAY CHARGING SOUND
            }
        }
        if (!Input.GetButton(pc_GunAttack.Name) && shooting)//if button not currently pressed but was last check, shoot
        {
            pc_GunAttack.Execute(player);
            shooting = false;//update press check
        }
    }

    private void SwordAttack()
    {
        if (Input.GetButtonDown(pc_SwordAttack.Name))
        {
            pc_SwordAttack.Execute(player);
        }
    }

    private void Dash()
    {
        if(Input.GetButtonDown(pc_Dash.Name))
        {
            pc_Dash.Execute(player);
        }
    }

    /// <summary>
    /// //calls the player's move function and passes in the input values
    /// </summary>
    private void Move()
    {
        if (Input.GetButton(pc_Move.XName) || Input.GetButton(pc_Move.YName))//if any of the move buttons are being pressed
        {
            pc_Move.Execute(player, Input.GetAxis(pc_Move.XName), Input.GetAxis(pc_Move.YName));
            anim.SetBool("Idle", false);//moving animation
        }
        else
        {
            anim.SetBool("Idle", true);//idle animation
        }
    }
}
