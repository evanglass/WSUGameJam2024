using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private float maxRange;
    public bool CanSeePlayer(Player player) {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        Debug.DrawRay(ray.origin, ray.direction * maxRange);
        if(Physics.Raycast(ray, out RaycastHit hit, maxRange, LayerMask.GetMask("Player", "Obstacles"))) {
            if (hit.transform.tag == "Player") {
                return true;
            }
        }
        return false;
    }


}
