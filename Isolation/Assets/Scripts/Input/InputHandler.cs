using System;
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
    GunCharge pc_GunCharge;
    Idle pc_Idle;

    //ButtonCommands
    ToggleDebug bc_Toggle;

    Player player;
    IButtonListener panelListener;

    private bool shooting = false;
    private float shootTimer, shootMinWaitTime = .567f;//the length of the charge animation//wait until the gun is moved upwards to shoot

    private void Start()
    {
        pc_Move = new Move();
        pc_Idle = new Idle();
        pc_GunCharge = new GunCharge();
        pc_GunAttack = new GunAttack();
        pc_SwordAttack = new SwordAttack();
        bc_Toggle = new ToggleDebug();
        pc_Dash = new Dash();

        player = GetComponent<Player>();

        panelListener = GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<IButtonListener>();//find the listener on the debug panel

        bc_Toggle.Listener = panelListener;

        SetCommandNames();
    }

    private void Update()
    {
        Move();
        Dash();
        GunAttack();
        GunHoldCheck();
        SwordAttack();
        ToggleDebugPanel();
    }

    private void GunHoldCheck()
    {
        //Can we move?
        if (shooting)
        {
            pc_GunCharge.Execute(player, Input.GetAxis(pc_Move.XName), Input.GetAxis(pc_Move.YName));//rotate while charging
            shootTimer += Time.deltaTime;
        }
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
                pc_GunCharge.Execute(player);//charging start
                shooting = true;//button is pressed
            }
        }
        if (!Input.GetButton(pc_GunAttack.Name) && shooting)//if button not currently pressed but was last check, shoot
        {
            if (shootTimer >= shootMinWaitTime)//make sure the windup animation has played
            {
                shootTimer = 0;//reset timer
                pc_GunAttack.Execute(player);//shoot
                shooting = false;//update press check
            }
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
    /// //calls the player's move function if canMove and passes in the input values
    /// </summary>
    private void Move()
    {
        //Move the amount that we have input for
        if ((Input.GetButton(pc_Move.XName) || Input.GetButton(pc_Move.YName)) && !shooting)//if any of the move buttons are being pressed
        {
            pc_Move.Execute(player, Input.GetAxis(pc_Move.XName), Input.GetAxis(pc_Move.YName));
        }
        else if(!shooting)
        {
            pc_Idle.Execute(player);
        }
    }
}
