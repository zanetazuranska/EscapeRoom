using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ER
{
    public class GameState : BaseState
    {
        public GameState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == GameSceneManager.Instance.GetActiveScene().ToString())
                {
                    SceneManager.SetActiveScene(SceneManager.GetSceneAt(i));
                    break;
                }
            }

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
            GameSceneConnector.OnGameSceneLoaded.AddListener(HandleGameControllerConnectorLoaded);
        }

        private void HandleGameControllerConnectorLoaded(GameController gameController)
        {
            GameSceneConnector.OnGameSceneLoaded.RemoveListener(HandleGameControllerConnectorLoaded);

            // Yupi! mamy game controllera w stanie.
        }
    }
}
