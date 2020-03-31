using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed, RotationSpeed;

    [SerializeField][Tooltip("Angle of sight for enemy")] private float fieldOfVision = 110;
    [SerializeField][Tooltip("Maximum sight for enemy")] private int Sightdepth = 10;

    public DebugPlayer DebugPlayer;
    public DebugScreen screen;

    public LayerMask seeable;

    private bool canSeePlayer;
    private PatrolPoint[] patrolPoints;
    private GameObject enemyParent;
    private LayerMask player, wall;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = transform.root.gameObject;
        patrolPoints = enemyParent.GetComponentsInChildren<PatrolPoint>();//get other objects in the same level that have the patrolpoint script
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        screen.DisplayText("Can See player: " + DebugPlayerIsInLineOfSight(), 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))//if I am near player
        {
            anim.SetBool("CanSeePlayer", PlayerIsInLineOfSight(other));
        }
    }

    private bool PlayerIsInLineOfSight(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfVision * .5f)//player is in field of view
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, Sightdepth))//no obstruciton between them like walls
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }

    private bool DebugPlayerIsInLineOfSight()
    {
        Vector3 direction = DebugPlayer.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfVision * .5f)//player is in field of view
        {
            if (Physics.Raycast(transform.position, direction.normalized, seeable, Sightdepth))//no obstruciton between them like walls
            {
                return true;
            }
            else return false;
        }
        else return false;
    }


    public Vector3 GetPatrolPointPosition(int index)
    {
        return patrolPoints[index].transform.position;
    }

    public int GetPointCount()
    {
        return patrolPoints.Length;
    }
}
