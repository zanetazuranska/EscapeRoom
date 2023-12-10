using ER;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneConnector : MonoBehaviour
{
    public static UnityEvent<GameController> OnGameSceneLoaded = new UnityEvent<GameController>(); //unity event

    [SerializeField]
    private GameController _gameController;

    public void Start()
    {
        if(OnGameSceneLoaded != null)
        OnGameSceneLoaded.Invoke(_gameController);
    }
}
