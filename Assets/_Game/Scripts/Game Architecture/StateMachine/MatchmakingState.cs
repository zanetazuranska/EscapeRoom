using UnityEngine;

namespace ER
{
    public class MatchmakingState : BaseState
    {
        public MatchmakingState(int id) : base(id) { }


        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter MatchmakingState");
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {

        }
    }
}
