using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    [SerializeField] private EnemyState onActivateState;
    [SerializeField] private int room;

    protected override void OnEnable() {
        base.OnEnable();
        animator.SetTrigger("Idle");
    }

    public static void EnemyEnterRoom(int num)
    {
        foreach(IdleState state in FindObjectsOfType<IdleState>())
        {
            if(state.room == num)
            {
                state.ChangeState();
            }
        }
    }

    public void ChangeState()
    {
        brain.SwitchState(onActivateState);
    }

    protected void Update() {
        if (muzzle.CanSeePlayer(player)) {
            ChangeState();
        }
    }
}
