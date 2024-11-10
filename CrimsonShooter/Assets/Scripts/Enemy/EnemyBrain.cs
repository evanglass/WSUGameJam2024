using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour, ITakesShots {
    [SerializeField] private EnemyState startingState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private IKControl ikControl;
    private Muzzle muzzle;
    private Player player;
    private RagdollController rc;

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
            muzzle.GetComponent<AudioSource>().Play();
        }
    }
    private void OnEnable() {
        health = maxHealth;
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
        if (rc == null) {
            rc = GetComponentInParent<RagdollController>();
            if (rc == null) {
                Debug.Log("No RagdollControllerFound");
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

    public void Die() {
        DeadState deadState = GetComponentInChildren<DeadState>(true);
        if (deadState != null) {
            SwitchState(deadState);
        }

    }
    private void Update() {

        float forwardVelocity = Vector3.Dot(transform.forward, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        float rightVelocity = Vector3.Dot(transform.right, navMeshAgent.velocity.normalized) * navMeshAgent.velocity.magnitude;
        animator.SetFloat("ForwardVelocity", forwardVelocity);
        animator.SetFloat("RightVelocity", rightVelocity);


    }
    private float health;
    [SerializeField] private float maxHealth;

    public bool TakeShot(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
            return true;
        }
        return false;
    }
}
