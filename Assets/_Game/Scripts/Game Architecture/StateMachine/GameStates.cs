using ER;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates Instance { get; private set; }

    public enum GameStatesEnum
    {
        InitializeState = 0,
        MainMenuState = 1,
        MatchmakingState = 2,
        GameState = 3,
    }

    private InitializeState _initializeState = new InitializeState((int)GameStatesEnum.InitializeState);
    private MainMenuState _mainMenuState = new MainMenuState((int)GameStatesEnum.MainMenuState);
    private MatchmakingState _matchmakingState = new MatchmakingState((int)GameStatesEnum.MatchmakingState);
    private GameState _gameState = new GameState((int)GameStatesEnum.GameState);

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

    public InitializeState GetInitializeState()
    {
        return _initializeState;
    }

    public BaseState GetMatchmakingState()
    {
        return _matchmakingState;
    }

    public BaseState GetGameState()
    {
        return _gameState;
    }

    public BaseState GetMainMenuState()
    {
        return _mainMenuState;
    }
}
