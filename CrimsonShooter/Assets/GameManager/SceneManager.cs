using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private SceneDefinition menuScene;

    private SceneDefinition activeScene;

    public static SceneManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Destroy(gameObject);

        Instance = this;

        UnityEngine.SceneManagement.SceneManager.LoadScene(menuScene.SceneBuildIndex, LoadSceneMode.Additive);
    }

    public void ChangeScene(SceneDefinition scene)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(activeScene.SceneBuildIndex);

        // Loading bar?

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.SceneBuildIndex, LoadSceneMode.Additive);
        activeScene = scene;
    }

    public void NextScene()
    {
        if (activeScene.NextScene != null)
        {
            ChangeScene(activeScene.NextScene);
        }
    }
}
