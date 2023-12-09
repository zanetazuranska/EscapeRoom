using ER;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private Scene _activeScene;
    private Scene _pendingScene;

    public System.Action<Scene> OnSceneLoaded; //zmien na UnityEvent

    private bool _isSceneLoaded = false;
    private bool _unloadCurrentScenes = false;

    //Transitions between scenes
    [SerializeField] private Animator _sceneFadeAnimator;
    [SerializeField] private SceneFade _sceneFade;
    private bool _outAnimComplete;


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
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == scene.ToString())
            {
                Debug.LogWarningFormat("You try to load the same scene again, SceneName={0}", scene.ToString());

                return;
            }
        }

        _unloadCurrentScenes = unloadCurrentScenes;
        StartCoroutine("LoadSceneC", scene);
    }

    private IEnumerator LoadSceneC(Scene scene)
    {
        _isSceneLoaded = false;

        _outAnimComplete = false;
        _sceneFade.OnOutAnimComplete.AddListener(OnOutAnimCompleteHandler);
        _sceneFadeAnimator.SetBool("Out", true);

        yield return new WaitUntil(() => _outAnimComplete);

        _pendingScene = scene;

        AsyncOperation loadSceneRequest = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

        loadSceneRequest.completed += SceneLoadedCompleted; 
    }

    private void SceneLoadedCompleted(AsyncOperation asyncOperation)
    {
        _activeScene = _pendingScene;

        if (OnSceneLoaded != null)
        {
            OnSceneLoaded.Invoke(_activeScene);
        }

        _isSceneLoaded = true;
        _sceneFadeAnimator.SetBool("In", true);

        if(_unloadCurrentScenes)
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
                if(SceneManager.GetSceneAt(i) != null && SceneManager.GetSceneAt(i).buildIndex != (int)_activeScene)
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i).buildIndex);
            }
        }
    }

    private void OnOutAnimCompleteHandler ()
    {
        _outAnimComplete = true;
        _sceneFade.OnOutAnimComplete.RemoveListener(OnOutAnimCompleteHandler);
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
