using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDefinition
{
    [SerializeField]
    private int buildIndex;

    [SerializeField]
    private SceneDefinition nextScene;
    public SceneDefinition NextScene { get => nextScene; }

    public SceneDefinition(int buildIndex, SceneDefinition nextScene)
    {
        this.buildIndex = buildIndex;
        this.nextScene = nextScene;
    }

    public int SceneBuildIndex
    {
        get => buildIndex;
    }

}