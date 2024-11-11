using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableLimb : MonoBehaviour, ITakesShots
{
    [SerializeField] private EnemyBrain brain;
    [SerializeField] private RagdollController rc;
    [SerializeField] private Nerd nerd;
    public bool TakeShot(float damage) {
        if (brain != null) {
            return brain.TakeShot(damage);
        }else if (rc != null) {
            rc.TriggerRagdoll();
        }
        if (nerd != null) {
        nerd.ActivateChair();
        }
        return true;
    }
}
