using UnityEngine;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        public enum GameStates
        {
            InitializeState = 0,
            MainMenuState = 1,
            MatchmakingState = 2,
            GameState = 3,
        }

        public InitializeState _initializeState = new InitializeState((int)GameStates.InitializeState);
        public MainMenuState _mainMenuState = new MainMenuState((int)GameStates.MainMenuState);
        public MatchmakingState _matchmakingState = new MatchmakingState((int)GameStates.MatchmakingState);
        public GameState _gameState = new GameState((int)GameStates.GameState);

        private void Start()
        {
            _stateMachine.Start(_initializeState);
        }

        private void Update()
        {
            //Scene Manager Test
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.MENU, true);
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.GameScene, true);
            }

            _stateMachine.Update();
        }
    }
}
