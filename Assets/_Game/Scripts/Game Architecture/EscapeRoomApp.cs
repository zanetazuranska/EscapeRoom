using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public enum EStartAs
    {
        Host = 0,
        Client = 1,
    }

    public class EscapeRoomApp : MonoBehaviour
    {
        public static EscapeRoomApp Instance { get; private set; }

        private GameController _currentGameController;

        public UnityEvent OnHostSpawned = new UnityEvent();
        public UnityEvent OnClientSpawned = new UnityEvent();

        [SerializeField] private ApplicationFlowController _applicationFlowController;

        public EStartAs startAs;

        private void Awake()
        {
            Screen.SetResolution(1920, 1080, true);

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void SetCurrentGameController(GameController gameController)
        {
            _currentGameController = gameController;
        }

        public ApplicationFlowController GetAplicationFlowController()
        {
            return _applicationFlowController;
        }

    }
}
