using UnityEngine.Events;

namespace ER
{
    public class StateMachine
    {
        public UnityEvent OnStateChange = new UnityEvent();

        private GameStates.GameStatesEnum _currentGameStates;
        private GameStates.GameStatesEnum _previousGameState;
        private GameStates.GameStatesEnum _nextGameStates;

        private BaseState _currentState;

        public void Start(BaseState _currentGameState)
        {
            if (OnStateChange == null)
                OnStateChange = new UnityEvent();

            _currentState = _currentGameState;
            _currentGameState.Context = this;
            _currentGameState.EnterState(this);
            _currentGameStates = (GameStates.GameStatesEnum)_currentState.GetStateId();
        }

        public void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(BaseState state, GameStates.GameStatesEnum nextState)
        {
            _currentState.ExitState(this);

            OnStateChange.Invoke();

            _previousGameState = (GameStates.GameStatesEnum)_currentState.GetStateId();
            _nextGameStates = nextState;

            _currentState = state;
            _currentState.Context = this;
            state.EnterState(this);

            _currentGameStates = (GameStates.GameStatesEnum)_currentState.GetStateId();
        }

        public GameStates.GameStatesEnum GetCurrentGameState()
        {
            return _currentGameStates;
        }

        public GameStates.GameStatesEnum GetPreviousGameState()
        {
            return _previousGameState;
        }

        public GameStates.GameStatesEnum GetNextGameState()
        {
            return _nextGameStates;
        }

    }

}
