using UnityEngine;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        private void Start()
        {
            _stateMachine.Start(GameStates.Instance.GetInitializeState());
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
