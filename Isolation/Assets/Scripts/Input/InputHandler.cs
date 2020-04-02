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
    Interact pc_Interact;

    //ButtonCommands
    ToggleDebug bc_Toggle;

    Player player;
    IButtonListener panelListener;

    private bool shooting = false;
    [SerializeField] private float shootTimer, shootMinWaitTime = .334f;//the length of the charge animation//wait until the gun is moved upwards to shoot
    [Tooltip("Attack = West, Interact = South, Dash = L Bumper, Inventory = R Bumper, Pause = Start Btn")]
    [SerializeField] private string horiInput, vertInput, attackInput, interactInput, shootInput, dashInput, inventoryInput, pauseInput;

    private void Start()
    {
        pc_Move = new Move();
        pc_GunCharge = new GunCharge();
        pc_GunAttack = new GunAttack();
        pc_SwordAttack = new SwordAttack();
        bc_Toggle = new ToggleDebug();
        pc_Dash = new Dash();
        pc_Interact = new Interact();

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
        SwordAttack();
        ToggleDebugPanel();
        Interact();

        if (shooting)
            shootTimer += Time.deltaTime;
    }

    private void SetCommandNames()
    {
        pc_Move.XName = "Horizontal";
        pc_Move.YName = "Vertical";
        pc_Dash.Name = "Dash";
        pc_SwordAttack.Name = "Slash";
        pc_GunAttack.Name = "Shoot";
        bc_Toggle.Name = "Toggle";
        pc_Interact.Name = "Interact";
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
        pc_Move.Execute(player, Input.GetAxis(pc_Move.XName), Input.GetAxis(pc_Move.YName));
    }

    private void Interact()
    {
        if (Input.GetButtonDown(pc_Interact.Name))
        {
            pc_Interact.Execute(player);
        }
    }
}
