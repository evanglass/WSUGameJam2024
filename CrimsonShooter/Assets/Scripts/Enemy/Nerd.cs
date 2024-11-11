using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nerd : MonoBehaviour
{
    [SerializeField] private Rigidbody chairRB;

    public void ActivateChair() {
        chairRB.isKinematic = false;
    }
}
