using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3, dashSpeed = 6, dashDuration = .5f;
    [SerializeField] private DebugScreen debugScreen;
    [SerializeField] private GameObject bulletInstance, bulletSpawnPoint;

    private AudioManager audio;
    private Gun gun;
    private PlayerStateMachine state;
    private bool canDash;

    private void Start()
    {
        state = GetComponent<PlayerStateMachine>();
        audio = AudioManager.instance;
        gun = GetComponentInChildren<Gun>();
    }

    public void Move(float x, float y)
    {
        debugScreen.DisplayText("Move Input: ( " + x.ToString() + ", " + y.ToString() + " )", 1);

        //if can rotate
        if (state.CanRotate)
        {     
            //if no move input, go idle
            if (Mathf.Abs(x) < .1f && Mathf.Abs(y) < .1f && !state.Charging)//if our input is so small, it doens't pick up movement and we're not charging a gun
            {
                state.SetState(State.idle);
            }
            //otherwise if can move, move
            else if(state.CanMove == true)
            {
                //rotate
                Vector3 moveDir = dir(x, y);
                transform.rotation = Quaternion.LookRotation(moveDir);

                //move
                transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
                state.SetState(State.walking);
            }
            else
            {
                //rotate
                Vector3 moveDir = dir(x, y);
                transform.rotation = Quaternion.LookRotation(moveDir);
            }      
        }
    }

    public void Interact()
    {
        state.SetState(State.interacting);
    }

    public void Dash()
    {
        if (canDash)
        {
            state.SetState(State.dashing);
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        for (float f = 0; f < dashDuration; f += Time.deltaTime)
        {
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        canDash = true;
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
