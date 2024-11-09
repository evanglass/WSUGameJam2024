using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    [SerializeField] private EnemyState onActivateState;

    protected override void OnEnable() {
        base.OnEnable();
        animator.SetTrigger("Idle");
    }
    protected void Update() {
        if (muzzle.CanSeePlayer(player)) {
            brain.SwitchState(onActivateState);
        }
    }
}
