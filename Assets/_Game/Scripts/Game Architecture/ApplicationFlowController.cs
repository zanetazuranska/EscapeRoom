using UnityEngine;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        private void Start()
        {
            if(GameStates.Instance.GetInitializeState().canEnter)
            {
                _stateMachine.Start(GameStates.Instance.GetInitializeState());
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
