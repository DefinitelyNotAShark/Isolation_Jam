﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyState : StateMachineBehaviour
{
    //PATHFINDING REFS
    private GameObject pathfindingObject;
    private GameObject player;
    private Pathfinding pathScript;
    private GridManager gridScript;
    private Transform transform;
    private Vector3 playerPosition;
    private float rotationSpeed, moveSpeed;
    private float minDistanceToShoot;

    private Enemy enemy;
    private int nodeIndex = 0;
    private List<Node> path;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //GET OBJECTS THAT CONTAIN PROPERTIES
        enemy = animator.GetComponent<Enemy>();
        pathfindingObject = GameObject.FindGameObjectWithTag("Pathfinder");
        player = GameObject.FindGameObjectWithTag("Player");

        //GET GHOST PROPERTIES
        transform = enemy.transform;
        rotationSpeed = enemy.RotationSpeed;
        moveSpeed = enemy.MoveSpeed;
        minDistanceToShoot = enemy.MinDistanceToShoot;

        //GET PATHFINDING PROPERTIES
        pathScript = pathfindingObject.GetComponent<Pathfinding>();
        gridScript = pathfindingObject.GetComponent<GridManager>();

        path = new List<Node>();

        //FIND PATH TO HIDING SPOT
        GetNewPoint();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        UpdatePoint();

        if (path.Count > minDistanceToShoot)//if the path is long enough to navigate
        {
            if (haveReachedNextNode() && nodeIndex < path.Count - 1)
                nodeIndex++;//get new node 


            //ROTATE        
            Quaternion targetRotation = Quaternion.LookRotation(path[nodeIndex].position - transform.position);//find where it's heading
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//set the rotation to move towards where it's headed 
            //MOVE
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }

        else//otherwise this means we're close enough to be caught
        {
            //ATTACK
            animator.SetTrigger("Attack");
        }
    }

    private bool haveReachedNextNode()
    {
        if (Vector3.Distance(transform.position, path[nodeIndex].position) < 1)//if we're close enough to the next node
            return true;

        else return false;
    }

    private void UpdatePoint()//checks if the player has moved and gets the new location if yes (stops from constantly updating)
    {
        if (player.transform.position != playerPosition)//if the actual player position is different than the position we're keeping track of    
            GetNewPoint();
    }

    private void GetNewPoint()
    {
        nodeIndex = 0;//reset the index
        playerPosition = player.transform.position;
        pathScript.FindPath(transform.position, playerPosition);//find path to the position
        path = gridScript.FinalPath;
    }
}
