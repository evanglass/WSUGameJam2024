﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneDefinitionContainer : MonoBehaviour
{
    [SerializeField]
    private SceneAsset scene;

    [SerializeField]
    private SceneDefinitionContainer nextScene;

    public SceneDefinition SceneDefinition { get; private set; }

    public void Compile()
    {
        Compile(new List<SceneDefinitionContainer>());
    }

    public void Compile(List<SceneDefinitionContainer> traversed)
    {
        traversed.Add(this);

        if (nextScene != null && !traversed.Contains(nextScene))
        {
            nextScene.Compile(traversed);
            SceneDefinition = new SceneDefinition(scene, nextScene.SceneDefinition);
        }
        else
        {
            SceneDefinition = new SceneDefinition(scene, null);
        }
    }

    public void StartScene()
    {
        Compile();
        SceneManager.Instance.ChangeScene(this.SceneDefinition);
    }
}