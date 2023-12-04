using ER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneConnector : MonoBehaviour
{
    public static System.Action<GameController> OnGameSceneLoaded; //unity event

    [SerializeField]
    private GameController _gameController;

    public void Start()
    {
        if(OnGameSceneLoaded != null)
        OnGameSceneLoaded.Invoke(_gameController);
    }
}
