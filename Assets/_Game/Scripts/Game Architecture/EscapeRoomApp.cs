using UnityEngine;

namespace ER
{
    public class EscapeRoomApp : MonoBehaviour
    {
        public static EscapeRoomApp Instance { get; private set; }

        private GameController _currentGameController;

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

    }
}
