using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class GameSceneConnector : MonoBehaviour
    {
        public static UnityEvent<GameController> OnGameSceneLoaded = new UnityEvent<GameController>();

        [SerializeField]
        private GameController _gameController;

        private void Start()
        {
            if (OnGameSceneLoaded != null)
                OnGameSceneLoaded.Invoke(_gameController);
        }
    }
}
