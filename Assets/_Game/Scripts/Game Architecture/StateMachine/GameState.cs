using UnityEngine;

namespace ER
{
    public class GameState : BaseState
    {
        public GameState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter GameState");

            GameSceneManager.Instance.OnSceneLoaded.AddListener(HandleGameSceneLoaded);
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {

        }

        private void HandleGameSceneLoaded(GameSceneManager.Scene scene)
        {
            GameSceneManager.Instance.OnSceneLoaded.RemoveListener(HandleGameSceneLoaded);
            GameSceneConnector.OnGameSceneLoaded = HandleGameControllerConnectorLoaded;
        }

        private void HandleGameControllerConnectorLoaded(GameController gameController)
        {
            // Yupi! mamy game controllera w stanie.
        }
    }
}
