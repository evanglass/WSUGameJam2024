using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyState : MonoBehaviour
{
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected Muzzle muzzle;
    protected Player player;
    protected EnemyBrain brain;

    protected virtual void OnEnable() {
        if (brain == null) {
            brain = GetComponentInParent<EnemyBrain>();
            if(brain == null) {
                Debug.Log("No brain found.");
            }
        }
        if(animator == null) {
            animator = GetComponentInParent<Animator>();
            if (animator == null) { Debug.Log("No animator found."); }
        }
        if (navMeshAgent == null) {
            navMeshAgent = GetComponentInParent<NavMeshAgent>();
            if (navMeshAgent == null) { Debug.Log("No NavMeshAgent found."); }

        }
        if (muzzle == null) {
            muzzle = animator.GetComponentInChildren<Muzzle>();
            if (muzzle == null) { Debug.Log("No Muzzle found."); }

        }
        if (player == null) {
            player = FindAnyObjectByType<Player>();
            if (player == null) {
                Debug.Log("No Player found");
            }
        }
    }

}
