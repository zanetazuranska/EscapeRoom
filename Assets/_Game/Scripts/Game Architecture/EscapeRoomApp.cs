using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRoomApp : MonoBehaviour
{
    public static EscapeRoomApp Instance { get; private set; }

    private GameController _currentGameController;

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

    public void SetCurrentGameController(GameController gameController)
    {
        _currentGameController = gameController;
    }

}
