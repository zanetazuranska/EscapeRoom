using UnityEngine;

namespace ER
{
    public class MainMenuState : BaseState
    {
        public MainMenuState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter MainMenuState");
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {

        }
    }
}