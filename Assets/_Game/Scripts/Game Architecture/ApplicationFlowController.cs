using ER.Riddle;
using UnityEngine;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        private RiddleController _currentRiddleController;

        private void Start()
        {
            if(GameStates.Instance.GetInitializeState()._canEnter)
            {
                _stateMachine.Start(GameStates.Instance.GetInitializeState());
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void SetCurrentRiddleController(RiddleController riddleController)
        {
            _currentRiddleController = riddleController;
        }

        public RiddleController GetCurrentRiddleController()
        {
            return _currentRiddleController;
        }
    }
}
