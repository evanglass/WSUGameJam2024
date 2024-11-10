using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerVolume : MonoBehaviour
{
    public UnityEvent Touch;
    public UnityEvent Untouch;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Touch.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Untouch.Invoke();
        }
    }
}
