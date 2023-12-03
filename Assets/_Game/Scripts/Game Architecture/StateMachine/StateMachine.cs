using UnityEngine.Events;

namespace ER
{
    public class StateMachine
    {
        public UnityEvent OnStateChange;

        private ApplicationFlowController.GameStates _currentGameStates;
        private ApplicationFlowController.GameStates _previousGameState;
        private ApplicationFlowController.GameStates _nextGameStates;

        private BaseState _currentState;

        public void Start(BaseState _currentGameState)
        {
            if (OnStateChange == null)
                OnStateChange = new UnityEvent();

            _currentState = _currentGameState;
            _currentGameState.Context = this;
            _currentGameState.EnterState(this);
            _currentGameStates = (ApplicationFlowController.GameStates)_currentState.GetStateId();
        }

        public void Update()
        {
            _currentState.UpdateState(this);
        }

        public void ChangeState(BaseState state, ApplicationFlowController.GameStates nextState)
        {
            _currentState.ExitState(this);

            OnStateChange.Invoke();

            _previousGameState = (ApplicationFlowController.GameStates)_currentState.GetStateId();
            _nextGameStates = nextState;

            _currentState = state;
            _currentState.Context = this;
            state.EnterState(this);

            _currentGameStates = (ApplicationFlowController.GameStates)_currentState.GetStateId();
        }

        public ApplicationFlowController.GameStates GetCurrentGameState()
        {
            return _currentGameStates;
        }

        public ApplicationFlowController.GameStates GetPreviousGameState()
        {
            return _previousGameState;
        }

        public ApplicationFlowController.GameStates GetNextGameState()
        {
            return _nextGameStates;
        }

    }

}
