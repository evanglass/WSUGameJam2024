using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyState
{
    protected override void OnEnable() {
        base.OnEnable();
        rc.TriggerRagdoll();
        navMeshAgent.enabled = false;
    }
}
