using ER;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;

    private AsyncOperation _loadSceneRequest;

    private Scene _activeScene;
    private Scene _pendingScene;

    public UnityEvent<Scene> OnSceneLoaded = new UnityEvent<Scene>();

    private bool _isSceneLoaded = false;
    private bool _unloadCurrentScenes = false;

    //Transitions between scenes
    [Header("Transition Settings")]
    [SerializeField] private Animator _sceneFadeAnimator;
    [SerializeField] private SceneFade _sceneFade;
    [SerializeField] private GameObject _sceneFadeObj;
    private bool _outAnimComplete;

    //Loading 
    [Header("Loading Screen Settings")]
    [SerializeField] private TextMeshProUGUI _loadingPercentages;
    [SerializeField] private GameObject _loadingObj;
    private int _percentages = 0;

    public enum Scene
    {
        MENU = 1,
        GameScene = 2,
        MatchmakingScene = 3,
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

    private void Update()
    {
        if(!_isSceneLoaded)
            SetLoadingPercentages();
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

        _sceneFadeObj.SetActive(true);
        _sceneFade.OnOutAnimComplete.AddListener(OnOutAnimCompleteHandler);
        _sceneFade.OnInAnimComplete.AddListener(OnInAnimCompleteHandler);
        _sceneFadeAnimator.SetBool("Out", true);

        yield return new WaitUntil(() => _outAnimComplete);

        _pendingScene = scene;

        StartCoroutine("SetLoadingPercentages");
        _loadSceneRequest = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);

        _loadSceneRequest.completed += SceneLoadedCompleted;
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

        if (_unloadCurrentScenes)
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

        _loadingPercentages.text = "0 %";
        _loadingObj.SetActive(true);

        _sceneFade.OnOutAnimComplete.RemoveListener(OnOutAnimCompleteHandler);
    }

    private void OnInAnimCompleteHandler()
    {
        _sceneFade.OnInAnimComplete.RemoveListener(OnInAnimCompleteHandler);
        _sceneFadeObj.SetActive(false);
    }

    private IEnumerator SetLoadingPercentages()
    {
        yield return new WaitForSeconds(0.2f);

        if(_isSceneLoaded)
        {
            _loadingPercentages.text = "100 %";
            yield return new WaitForSeconds(0.1f);
            _loadingObj.SetActive(false);
            _loadingPercentages.text = "0 %";
        }
        else if (_percentages + 10 >= 100 && !_isSceneLoaded)
        {
            _loadingPercentages.text = _percentages.ToString() + " %";
            StartCoroutine("SetLoadingPercentages");
        }
        else
        {
            _percentages += 10;
            _loadingPercentages.text = _percentages.ToString() + " %";
            StartCoroutine("SetLoadingPercentages");
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

    public SceneFade GetSceneFade()
    {
        return _sceneFade;
    }
}
