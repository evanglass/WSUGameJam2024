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
        if (nextScene != null)
        {
            nextScene.Compile();
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
