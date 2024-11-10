using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JazzHands : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private void Update() {
        transform.position = targetTransform.position;
    }
}
