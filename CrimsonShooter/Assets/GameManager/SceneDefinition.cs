using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDefinition : MonoBehaviour
{
    [SerializeField]
    private SceneAsset scene;

    [SerializeField]
    private SceneDefinition nextScene;

    public int SceneBuildIndex
    { 
        get {
            string scenePath = "Assets/Scenes/" + scene.name + ".unity";
            return SceneUtility.GetBuildIndexByScenePath(scenePath);
        }  
    }

    public SceneDefinition NextScene { get => nextScene; }

    public void StartNextScene()
    {
        SceneManager.Instance.ChangeScene(this.NextScene);
    }
}
