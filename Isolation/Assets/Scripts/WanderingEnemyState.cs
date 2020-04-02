using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemyState : StateMachineBehaviour
{
    //Scripts
    private Enemy enemy;
    private Pathfinding pathScript;
    private GridManager gridScript;

    //Components
    private GameObject pathfindingObject;
    private Transform transform;
    private Vector3 patrolPointPosition;

    //Vars
    private float rotationSpeed;
    private float moveSpeed;
    private int patrolIndex;//index of patrol points
    private int pathIndex;//index pf nodes

    List<Node> path = new List<Node>();//this is our reference to the path that the manager finds

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //GET OBJECTS THAT CONTAIN PROPERTIES
        enemy = animator.GetComponent<Enemy>();
        pathfindingObject = GameObject.FindGameObjectWithTag("Pathfinder");

        //GET GHOST PROPERTIES
        transform = enemy.transform;
        rotationSpeed = enemy.RotationSpeed;
        moveSpeed = enemy.MoveSpeed;

        //GET PATHFINDING PROPERTIES
        pathScript = pathfindingObject.GetComponent<Pathfinding>();
        gridScript = pathfindingObject.GetComponent<GridManager>();

        pathIndex = 0;
        GetNewPoint();//find the path to the patrol point;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //IF AT PATROL POINT (if the distance is small enough to find a new point)
        if (Vector3.Distance(transform.position, patrolPointPosition) < 2)
        {
            if (patrolIndex == enemy.GetPointCount() - 1)//if the index is the max number (or equal to the count of our list (- 1 for indexing))
                patrolIndex = 0;//reset

            else patrolIndex++;//get a new target           

            GetNewPoint();
        }

        //IF AT END OF NODE WITH THE PATH TO THE PATROL POINT
        else if (Vector3.Distance(transform.position, path[pathIndex].position) < 1)//if we've reached our target node
        {
            if (pathIndex < path.Count - 1)//if we're not at the last node yet
                pathIndex++;//get the next node                  
        }

        //ROTATE
        Quaternion targetRotation = Quaternion.LookRotation(path[pathIndex].position - transform.position);//find where it's heading only y rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);//set the rotation to move towards where it's headed
        transform.rotation = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));//only rotate the y;;;
        //MOVE
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);//move forward. The rotation determines direction 
    }

    private void GetNewPoint()
    {
        pathIndex = 0;//reset the index
        patrolPointPosition = enemy.GetPatrolPointPosition(patrolIndex);//set the position we want
        pathScript.FindPath(transform.position, patrolPointPosition);//find path to the position    
        path = gridScript.FinalPath;//set local reference to the path it found
    }
}
