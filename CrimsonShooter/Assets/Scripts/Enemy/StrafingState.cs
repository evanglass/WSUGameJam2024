using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StrafingState : EnemyState
{
    [SerializeField] private float strafeDistance = 3f;
    [SerializeField] private float maxStrafeTime = 4f;
    [SerializeField] private EnemyState movingToPlayerState;
    protected override void OnEnable() {
        base.OnEnable();
        Debug.Log("Entered Strafe State");
        navMeshAgent.updateRotation = false;
        BeginStrafe();
    }
    private void Update() {
        navMeshAgent.transform.rotation = Utilities.GetRotationTowardsTarget(
            navMeshAgent.transform,
            player.transform,
            navMeshAgent.angularSpeed*2,
            Time.deltaTime);

        if (Utilities.HasReachedDestination(navMeshAgent) || Time.time - timeBeganStrafe > maxStrafeTime) {
            if (muzzle.CanSeePlayer(player)) {
                BeginStrafe();
            } else {
                brain.SwitchState(movingToPlayerState);
            }
        }
        animator.speed = navMeshAgent.velocity.magnitude * 0.4f;
    }

    private void BeginStrafe() {

        timeBeganStrafe = Time.time;
        strafeToggle = !strafeToggle;
        if (strafeToggle) {
            Utilities.SetAgentStrafeLeft(navMeshAgent, navMeshAgent.transform, player.transform, strafeDistance);

        } else {
            Utilities.SetAgentStrafeRight(navMeshAgent, navMeshAgent.transform, player.transform, strafeDistance);


        }
        Debug.DrawRay(navMeshAgent.pathEndPosition, Vector3.up, Color.red, 5f);

    }

    private bool strafeToggle;
    private float timeBeganStrafe;
    
}


