using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle,
    walking,
    running,
    chargingGun,
    firingGun,
    slashing,
    interacting,
    dashing
}

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private DebugScreen debug;

    [HideInInspector] public bool CanMove;
    [HideInInspector] public bool CanRotate;

    private State PlayerState;
    private Animator anim;

    private bool charging = false;
    private bool IsMoving;

    private bool slashed = false;
    private bool fired = false;
    private bool interacted = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        PlayerState = State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerState)
        {
            case State.chargingGun:

                if (!charging)
                {
                    charging = true;
                    anim.SetTrigger("Charging");
                }

                fired = false;
                CanMove = false;
                CanRotate = true;
                break;
            case State.firingGun:

                if (!fired)
                {
                    anim.SetTrigger("ReleaseTrigger");
                    fired = true;
                }

                CanMove = false;
                CanRotate = false;
                charging = false;
                break;
            case State.idle:
                anim.SetBool("Idle", true);
                CanMove = true;
                CanRotate = true;

                //reset bools 
                fired = false;
                interacted = false;
                slashed = false;

                break;
            case State.interacting:
                if (!interacted)
                {
                    anim.SetTrigger("Interacting");
                    interacted=true;
                }
                CanMove = false;
                CanRotate = false;
                break;
            case State.running:
                CanMove = true;
                CanRotate = true;
                break;
            case State.slashing:
                if (!slashed)
                {
                    anim.SetTrigger("Slashing");
                    slashed = true;
                }
                CanMove = false;
                CanRotate = false;
                break;
            case State.walking:
                anim.SetBool("Idle", false);
                CanMove = true;
                CanRotate = true;
                break;
            case State.dashing:
                CanMove = false;
                CanRotate = false;
                break;      
        }
        debug.DisplayText("Can Move: " + CanMove.ToString(), 0);
        debug.DisplayText("State: " + PlayerState, 2);

        if (charging)
        {
            SetState(State.chargingGun);
        }
    }

    public void ReturnToIdle()
    {
        PlayerState = State.idle;
    }

    public void SetState(State newState)
    {
        PlayerState = newState;
    }

    public State GetState()
    {
        return PlayerState;
    }
}
