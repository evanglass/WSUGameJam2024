using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDefinition
{
    [SerializeField]
    private SceneAsset scene;

    [SerializeField]
    private SceneDefinition nextScene;
    public SceneDefinition NextScene { get => nextScene; }

    public SceneDefinition(SceneAsset scene, SceneDefinition nextScene)
    {
        this.scene = scene;
        this.nextScene = nextScene;
    }

    public int SceneBuildIndex
    {
        get
        {
            string scenePath = "Assets/Scenes/" + scene.name + ".unity";
            return SceneUtility.GetBuildIndexByScenePath(scenePath);
        }
    }

}