using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Camera loadingCamera;

    [SerializeField]
    private SceneDefinitionContainer menuScene;

    private SceneDefinition activeScene;

    public static SceneManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;

        menuScene.Compile();

        UnityEngine.SceneManagement.SceneManager.LoadScene(menuScene.SceneDefinition.SceneBuildIndex, LoadSceneMode.Additive);

        activeScene = menuScene.SceneDefinition;

        UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnUnloadScene;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnUnloadScene(Scene scene)
    {

    }

    private void OnLoadScene(Scene scene, LoadSceneMode lsm)
    {

    }

    public void ChangeScene(SceneDefinition scene)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(activeScene.SceneBuildIndex);

        // Loading bar?

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.SceneBuildIndex, LoadSceneMode.Additive);
        activeScene = scene;
    }

    public void ReloadScene()
    {
        ChangeScene(activeScene);
    }

    public void NextScene()
    {
        if (activeScene.NextScene != null)
        {
            ChangeScene(activeScene.NextScene);
        }
    }
}
