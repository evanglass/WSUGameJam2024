using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.Instance.NextScene();
    }
}
