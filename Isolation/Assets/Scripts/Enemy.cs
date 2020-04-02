using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed, RotationSpeed;
    public int MinDistanceToShoot;

    [SerializeField][Tooltip("Angle of sight for enemy")] private float fieldOfVision = 110;
    [SerializeField][Tooltip("Maximum sight for enemy")] private int Sightdepth = 10;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))//if I am near player
        {
            anim.SetBool("CanSeePlayer", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("CanSeePlayer", false);
        }
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
