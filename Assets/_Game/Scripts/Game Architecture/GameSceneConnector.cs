using ER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneConnector : MonoBehaviour
{
    public static System.Action<GameController> OnGameSceneLoaded;

    [SerializeField]
    private GameController m_GameController;

    public void Start()
    {
        if(OnGameSceneLoaded != null)
        OnGameSceneLoaded.Invoke(m_GameController);
    }
}
