using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPlayerState : EnemyState
{
    [SerializeField] private EnemyState strafeState;
    protected override void OnEnable() {
        base.OnEnable();


        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.updateRotation = true;

    }
    private void Update() {
        if (muzzle.CanSeePlayer(player)) {
            brain.SwitchState(strafeState);
            brain.SetAimTarget(player.GetCenterOfMass());
        }
    }
    private float navTime = 0.5f;
    private float navTimer;
}
