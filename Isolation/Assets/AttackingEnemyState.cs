using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemyState : StateMachineBehaviour
{
    private Player player;
    private Transform playerTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3.RotateTowards(animator.gameObject.transform.position, playerTransform.position, 3, 1);
    }
}
