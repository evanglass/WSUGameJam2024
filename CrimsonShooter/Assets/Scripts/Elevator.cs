using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool initialized = false;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Elevator").GetComponent<Animator>().SetBool("Open", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!initialized)
        {
            initialized = true;
            return;
        }
        if (other.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Elevator").GetComponent<Animator>().GetBool("Open"))
            {
                GameObject.FindGameObjectWithTag("Elevator").GetComponent<Animator>().SetBool("Open", false);
            }
        }
    }
}
