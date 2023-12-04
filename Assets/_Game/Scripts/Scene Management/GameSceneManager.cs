using ER;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private Scene _activeScene;
    private Scene _pendingScene;

    public System.Action<Scene> OnSceneLoaded; //zmien na UnityEvent

    private bool _isSceneLoaded = false;

    public enum Scene
    {
        MENU = 1,
        GameScene = 2,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void LoadScene(Scene scene, bool unloadCurrentScenes)
    {
        for(int i = 0; i<SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == scene.ToString())
            {
                Debug.LogWarningFormat("You try to load the same scene again, SceneName={0}", scene.ToString());
                return;
            }
        }

        _isSceneLoaded = false;

        _pendingScene = scene;

        AsyncOperation loadSceneRequest = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

        loadSceneRequest.completed += SceneLoadedCompleted; 

        //zrob to w completed+=
        if (unloadCurrentScenes)
        {
            UnloadScenes();
        }
    }

    private void SceneLoadedCompleted(AsyncOperation asyncOperation)
    {
        _activeScene = _pendingScene;

        if (OnSceneLoaded != null)
        {
            OnSceneLoaded.Invoke(_activeScene);
        }
    }

    private void UnloadScenes()
    {
        if (SceneManager.sceneCount > 1)
        {
            for (int i = 1; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i) != null)
                SceneManager.UnloadSceneAsync(i);
            }
        }
    }

    public Scene GetActiveScene()
    {
        return _activeScene;
    }

    public void IsSceneLoaded()
    {
        _isSceneLoaded = true;
    }
}
