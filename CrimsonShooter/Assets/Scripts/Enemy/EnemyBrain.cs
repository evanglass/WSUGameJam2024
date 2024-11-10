using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour {
    [SerializeField] private EnemyState startingState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private IKControl ikControl;
    private Muzzle muzzle;
    private Player player;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private EnemyProjectile enemyProjectilePrefab;
    [SerializeField] private float innaccuracy;
    public void Shoot(Transform target) {
        if (ikControl != null) {
            ikControl.Fire();
        }
        if (muzzle == null) {
            return;
        }
        if (muzzleFlash != null) { 
            Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
        }
        if(enemyProjectilePrefab != null) {
            EnemyProjectile proj = Instantiate(enemyProjectilePrefab, muzzle.transform.position, muzzle.transform.rotation);
            Vector3 dirToTarget = (target.position - muzzle.transform.position).normalized;

            dirToTarget += Random.insideUnitSphere * innaccuracy;
            dirToTarget.Normalize();
            proj.Fire(dirToTarget);
        }
    }
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
        if (muzzle == null) {
            muzzle = animator.GetComponentInChildren<Muzzle>();
            if (muzzle == null) { Debug.Log("No Muzzle found."); }

        }
        if (ikControl == null) {
            ikControl = GetComponentInParent<IKControl>();
            if (ikControl == null) {
                Debug.Log("No IKControl found.");

            }
        }
        if (player == null) {
            player = FindAnyObjectByType<Player>();
            if (player == null) {
                Debug.Log("No Player found");
            }
        }
    }

    public void SetAimTarget(Transform aimTarget) {
        ikControl.SetAimTarget(aimTarget);
    }

    public void SwitchState(EnemyState state) {
        EnemyState[] states = GetComponentsInChildren<EnemyState>();
        foreach (EnemyState s in states) {
            s.gameObject.SetActive(false);
            state.gameObject.SetActive(true);
        }
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.T)) {
            Shoot(player.GetCenterOfMass());
        }


        float forwardVelocity = Vector3.Dot(transform.forward, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        float rightVelocity = Vector3.Dot(transform.right, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        animator.SetFloat("ForwardVelocity", forwardVelocity);
        animator.SetFloat("RightVelocity", rightVelocity);


    }
}
