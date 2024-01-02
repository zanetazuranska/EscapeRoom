using UnityEngine;

namespace ER
{
    public class EscapeRoomApp : MonoBehaviour
    {
        public static EscapeRoomApp Instance { get; private set; }

        private GameController _currentGameController;

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
