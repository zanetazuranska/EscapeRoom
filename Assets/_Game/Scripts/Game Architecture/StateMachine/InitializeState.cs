using UnityEngine;

namespace ER
{
    public class InitializeState : BaseState
    {
        public InitializeState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter InitializeState");
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {
            
        }
    }
}