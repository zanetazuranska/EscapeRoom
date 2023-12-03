using ER;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private Scene activeScene;

    public System.Action<Scene> OnSceneLoaded;

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
            if(SceneManager.GetSceneAt(i).buildIndex == (int)scene)
            {
                Debug.Log("You try to load the same scene again");
                return;
            }
        }

        _isSceneLoaded = false;

        SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
        activeScene = scene;

        if(OnSceneLoaded != null)
        {
            OnSceneLoaded.Invoke(activeScene);
        }

        if (unloadCurrentScenes)
        {
            UnloadScenes();
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
        return activeScene;
    }

    public void IsSceneLoaded()
    {
        _isSceneLoaded = true;
    }
}
