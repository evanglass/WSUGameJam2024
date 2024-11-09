using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float onTime;

    [SerializeField]
    private float offTime;

    [SerializeField]
    private bool startOn;

    public UnityEvent<bool> OnTimerFire;
    public UnityEvent OnTimerFireOn;
    public UnityEvent OnTimerFireOff;

    private bool on;

    private float lastFireTime;

    // Start is called before the first frame update
    void Start()
    {
        on = startOn;
        lastFireTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            if (Time.realtimeSinceStartup - lastFireTime > offTime)
            {
                on = false;
                OnTimerFire.Invoke(on);
                OnTimerFireOff.Invoke();
                lastFireTime = Time.realtimeSinceStartup;
            }
        }
        else
        {
            if (Time.realtimeSinceStartup - lastFireTime > onTime)
            {
                on = true;
                OnTimerFire.Invoke(on);
                OnTimerFireOn.Invoke();
                lastFireTime = Time.realtimeSinceStartup;
            }
        }
    }
}
