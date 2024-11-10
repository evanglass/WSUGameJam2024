using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnCameraShake;
    public void ShakeCamera() {
        OnCameraShake?.Invoke(this, EventArgs.Empty);
    }
    public static GameManager Instance;

    
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
    }

    #region GameOver



    #endregion
}
