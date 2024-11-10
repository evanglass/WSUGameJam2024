using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenComputer : MonoBehaviour
{
    private void OnEnable() {
        Debug.Log("broken computer spawned");
        GameManager.Instance.ShakeCamera();
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs) {
            rb.AddExplosionForce(5f, transform.position, 5f);
        }
    }
}
