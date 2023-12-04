using ER;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private ER.GameController _gameController; 

    private void Start()
    {
        GameSceneManager.Instance.IsSceneLoaded();

        if(_gameController != null)
        EscapeRoomApp.Instance.SetCurrentGameController(_gameController);
    }
}
