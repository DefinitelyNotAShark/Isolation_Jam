﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Stats Stats;
   
    [SerializeField] private float speed = 3, dashSpeed = 6, idleDashDuration = .5f, walkingDashDuration = 1.2f;
    [SerializeField] private float startingHealth = 100, startingEnergy = 100, startingPower = 100;
    [SerializeField] private DebugScreen debugScreen;
    [SerializeField] private GameObject bulletInstance, bulletSpawnPoint;

    private AudioManager audio;
    private Gun gun;
    private PlayerStateMachine state;
    private bool coroutineStarted = false;

    /// <summary>
    /// Checks if player has the energy to dash
    /// </summary>
    /// <returns>True if the player's energy is higher than 0</returns>
    private bool CanDash()
    {
        if (Stats.Energy > 0)
            return true;
        else return false;
    }

    private void Awake()
    {
        Stats = new Stats(startingHealth, startingEnergy, startingPower);

        state = GetComponent<PlayerStateMachine>();
        audio = AudioManager.instance;
        gun = GetComponentInChildren<Gun>();
    }

    public void Move(float x, float y)
    {
        debugScreen.DisplayText("Move Input: ( " + x.ToString() + ", " + y.ToString() + " )", 1);

        //if can rotate
        if (state.CanRotate == true)
        {
            //rotate  
            Vector3 moveDir = dir(x, y);

            //otherwise if can move, move
            if (state.CanMove == true)
            {
                //move if there's input
                if (moveDir != Vector3.zero)
                {
                    transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
                    state.SetState(State.walking);
                }
                else if (state.GetState() == State.walking)//if we were walking and now we aren't
                {
                    state.ReturnToIdle();//go idle
                }
            }        
            if(moveDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        
    }

    public void Interact()
    {
        state.SetState(State.interacting);
    }

    public void Dash()
    {

        if (CanDash() && !coroutineStarted)
        {
            float dashDuration = idleDashDuration;//default idle time

            if (state.GetState() == State.idle)
                dashDuration = idleDashDuration;
            else if (state.GetState() == State.walking)
                dashDuration = walkingDashDuration;
          
            StartCoroutine(DashCoroutine(dashDuration));
            state.SetState(State.dashing);
        }
    }

    private IEnumerator DashCoroutine(float duration)
    {
        coroutineStarted = true;
        for (float f = 0; f < duration; f += Time.deltaTime)
        {
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        coroutineStarted = false;

        state.ReturnToIdle();
    }

    public void GunAttack()
    {
        state.SetState(State.firingGun);
        audio.StopSound("Charge");
        audio.PlaySound("Shoot");
        gun.Shoot();
    }

    public void GunCharge()//first time the button is down
    {
        state.SetState(State.chargingGun);
        audio.PlaySound("Charge");
        gun.Charge();
    }

    public void SwordAttack()
    {
        state.SetState(State.slashing);
    }

    //this is called by the animation when the actual slash happens
    public void SwordSlash()
    {
        audio.PlaySound("Slash");
    }

    #region direction check
    private Vector3 dir(float x, float y)
    {
        if (x == 0 && y == 0)//first check for no input
            return new Vector3(0, 0, 0);
        if (y == 0)//if only pressing horizontal
        {
            if (x < 0)
                return new Vector3(-1, 0, 0);//left      

            if(x > 0)
                return new Vector3(1, 0, 0);//right
        }
        else if (x == 0)//if only pressing vertical
        {
            if (y < 0)
                return new Vector3(0, 0, -1);//down
            if(y > 0)
                return new Vector3(0, 0, 1);//up
        }
        else if (y < 0)//downs
        {
            if (x < 0)
                return new Vector3(-1, 0, -1);//down left
            if (x > 0)
                return new Vector3(1, 0, -1);//down right
        }
        else//ups
        {
            if (x < 0)
                return new Vector3(-1, 0, 1);//up left
            if (x > 0)
                return new Vector3(1, 0, 1);//up right
        }
        return new Vector3(0, 0, 0);//default no movement
    }
    #endregion
}
