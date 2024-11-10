using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableLimb : MonoBehaviour, ITakesShots
{
    [SerializeField] private EnemyBrain brain;

    public bool TakeShot(float damage) {
        return brain.TakeShot(damage);
    }
}
