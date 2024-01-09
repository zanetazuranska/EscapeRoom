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


            if(EscapeRoomApp.Instance.startAs == EscapeRoomApp.StartAs.Host)
            {
                GameNetworkData gameData = NetworkSessionManager.Instance.GetGameNetworkData(GameNetworkData.ENetMode.Host);

                NetworkSessionManager.Instance.SetUnityTransport(gameData.portNum, gameData.iPAddress);

                NetworkManager.Singleton.StartHost();
            }
            else
            {
                GameNetworkData gameData = NetworkSessionManager.Instance.GetGameNetworkData(GameNetworkData.ENetMode.Client);

                NetworkSessionManager.Instance.SetUnityTransport(gameData.portNum, gameData.iPAddress);

                NetworkManager.Singleton.StartClient();
            }
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

            Debug.Log("GameController");
            // Yupi! mamy game controllera w stanie.
        }
    }
}
