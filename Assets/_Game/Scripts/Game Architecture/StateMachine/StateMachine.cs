
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR.Haptics;

namespace ER
{
    public class StateMachine
    {
        public enum GameStates
        {
            InitializeState = 0,
            MainMenuState = 1,
            MatchmakingState = 2,
            GameState = 3,
        }
        private GameStates _currentGameStates;

        private BaseState _currentState;
        public InitializeState _initializeState = new InitializeState((int)GameStates.InitializeState);
        public MainMenuState _mainMenuState = new MainMenuState((int)GameStates.MainMenuState);
        public MatchmakingState _matchmakingState = new MatchmakingState((int)GameStates.MatchmakingState);
        public GameState _gameState = new GameState((int)GameStates.GameState);


        public void Start()
        {
            _currentState = _initializeState;
            _initializeState.Context = this;
            _initializeState.EnterState(this);
            _currentGameStates = (GameStates)_currentState.GetStateId();
        }

        public void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(BaseState state)
        {
            _currentState.ExitState(this);

            _currentState = state;
            _currentState.Context = this;
            state.EnterState(this);

            _currentGameStates = (GameStates)_currentState.GetStateId();
        }

        public GameStates GetCurrentGameState()
        {
            return _currentGameStates;
        }

    }

}
