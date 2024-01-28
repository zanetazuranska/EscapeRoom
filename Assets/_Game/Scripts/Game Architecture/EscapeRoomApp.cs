using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class EscapeRoomApp : MonoBehaviour
    {
        public static EscapeRoomApp Instance { get; private set; }

        private GameController _currentGameController;

        public UnityEvent OnHostSpawned = new UnityEvent();
        public UnityEvent OnClientSpawned = new UnityEvent();

        [SerializeField] private Transform _winPrefab;
        private Transform _winTransform;

        public enum StartAs
        {
            Host = 0,
            Client = 1,
        }

        public StartAs startAs;

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

        [ServerRpc]
        public void WinServerRpc()
        {
            _winTransform = Instantiate(_winPrefab);
            _winTransform.GetComponent<NetworkObject>().Spawn(true);
        }

    }
}
