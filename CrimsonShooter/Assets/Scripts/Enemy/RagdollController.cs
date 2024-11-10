using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour {
    public Animator animator;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;

    private void OnEnable() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        // Find all Rigidbody and Collider components in the ragdoll hierarchy
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Start in animation mode, so disable ragdoll initially
        SetRagdollMode(false);

    }

    public void SetRagdollMode(bool isRagdoll) {
        // Enable or disable the animator
        animator.enabled = !isRagdoll;

        // Enable/Disable all ragdoll rigidbodies and colliders
        foreach (Rigidbody rb in ragdollRigidbodies) {
            rb.isKinematic = !isRagdoll;
        }

        //foreach (Collider col in ragdollColliders) {
        //    // Ignore the main collider if you have one (e.g., a capsule for the root)
        //    if (col.gameObject == gameObject) continue;

        //    col.enabled = isRagdoll;
        //}
    }

    public void TriggerRagdoll() {

        SetRagdollMode(true);
    }

}