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


            if(EscapeRoomApp.Instance.startAs == EStartAs.Host)
            {
                StartHost();
            }
            else
            {
                StartClient();
            }
        }

        private void StartHost()
        {
            GameNetworkData gameData = NetworkSessionManager.Instance.GetGameNetworkData(GameNetworkData.ENetMode.Host);

            NetworkSessionManager.Instance.SetUnityTransport(gameData.portNum, gameData.iPAddress);

            NetworkManager.Singleton.StartHost();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void StartClient()
        {
            GameNetworkData gameData = NetworkSessionManager.Instance.GetGameNetworkData(GameNetworkData.ENetMode.Client);

            NetworkSessionManager.Instance.SetUnityTransport(gameData.portNum, gameData.iPAddress);

            NetworkManager.Singleton.StartClient();

            Cursor.lockState = CursorLockMode.Locked;
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
