using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemyState : StateMachineBehaviour
{
    private Player player;
    private Transform playerTransform;

    //values for internal use
    private Quaternion lookRotation;
    private Vector3 direction;
    private Transform transform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform = animator.gameObject.transform;
    }

    private void RotateToFacePlayer(Animator animator, float speed)
    {
        //find the vector pointing from our position to the target
        direction = (playerTransform.position - animator.gameObject.transform.position).normalized;

        //create the rotation we need to be in to look at the target
        lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }
}
