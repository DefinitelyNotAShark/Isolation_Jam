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
    [HideInInspector] public bool Charging;

    private State PlayerState;
    private Animator anim;
    private bool IsMoving;


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
                if (!Charging)
                {
                    Charging = true;
                    anim.SetTrigger("Charging");
                }

                CanMove = false;
                CanRotate = true;
                break;
            case State.firingGun:
                anim.SetTrigger("ReleaseTrigger");
                CanMove = false;
                CanRotate = false;
                Charging = false;
                break;
            case State.idle:
                anim.SetBool("Idle", true);
                CanMove = true;
                CanRotate = true;
                break;
            case State.interacting:
                anim.SetTrigger("Interact");
                CanMove = false;
                CanRotate = false;
                break;
            case State.running:
                CanMove = true;
                CanRotate = true;
                break;
            case State.slashing:
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

        if (Charging)
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
