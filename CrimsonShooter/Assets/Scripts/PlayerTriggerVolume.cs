using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerVolume : MonoBehaviour
{
    [SerializeField] private bool once;
    
    public UnityEvent Touch;
    public UnityEvent Untouch;

    private bool hasTriggeredOn = false;
    private bool hasTriggeredOff = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !(once && hasTriggeredOn))
        {
            Debug.Log("Triggered");
            Touch.Invoke();
            hasTriggeredOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !(once && hasTriggeredOff))
        {
            Debug.Log("UnTriggered");
            Untouch.Invoke();
            hasTriggeredOff = true;
        }
    }
}
