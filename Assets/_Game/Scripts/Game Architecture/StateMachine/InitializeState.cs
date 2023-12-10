using UnityEngine;

namespace ER
{
    public class InitializeState : BaseState
    {
        public InitializeState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter InitializeState");

            GameSceneManager.Instance.OnSceneLoaded.AddListener(OnMenuLoad);
            GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.MENU, true);
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {
            
        }

        private void OnMenuLoad(GameSceneManager.Scene scene)
        {
            GameSceneManager.Instance.OnSceneLoaded.RemoveListener(OnMenuLoad);

            Context.ChangeState(GameStates.Instance.GetMainMenuState(), GameStates.GameStatesEnum.MatchmakingState);
        }
    }
}