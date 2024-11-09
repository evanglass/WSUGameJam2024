using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationExit : MonoBehaviour
{
    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
