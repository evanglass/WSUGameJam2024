using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour {
    [SerializeField] private EnemyState startingState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void OnEnable() {
        SwitchState(startingState);

        if (animator == null) {
            animator = GetComponentInParent<Animator>();
            if (animator == null) { Debug.Log("No animator found."); }
        }
        if (navMeshAgent == null) {
            navMeshAgent = GetComponentInParent<NavMeshAgent>();
            if (navMeshAgent == null) { Debug.Log("No NavMeshAgent found."); }

        }
    }

    public void SwitchState(EnemyState state) {
        EnemyState[] states = GetComponentsInChildren<EnemyState>();
        foreach (EnemyState s in states) {
            s.gameObject.SetActive(false);
            state.gameObject.SetActive(true);
        }
    }

    private void Update() {
        float forwardVelocity = Vector3.Dot(transform.forward, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        float rightVelocity = Vector3.Dot(transform.right, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        animator.SetFloat("ForwardVelocity", forwardVelocity);
        animator.SetFloat("RightVelocity", rightVelocity);


    }
}
