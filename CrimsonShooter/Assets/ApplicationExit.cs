using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ApplicationExit : MonoBehaviour
{
    public UnityEvent<bool> SetFromExitable;

    private void Awake()
    {
        SetFromExitable.Invoke(Application.platform != RuntimePlatform.WebGLPlayer);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
