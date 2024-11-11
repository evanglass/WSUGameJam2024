using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nerd : MonoBehaviour, ITakesShots
{
    [SerializeField] private Rigidbody chairRB;

    public void ActivateChair() {
        chairRB.isKinematic = false;
    }

    public bool TakeShot(float damage)
    {
        GetComponent<RagdollController>().SetRagdollMode(true);
        ActivateChair();
        chairRB.transform.parent = transform.root;
        return true;
    }
}
