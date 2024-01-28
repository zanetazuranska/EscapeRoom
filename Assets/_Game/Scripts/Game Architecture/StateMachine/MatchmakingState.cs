using UnityEngine;

namespace ER
{
    public class MatchmakingState : BaseState
    {
        public MatchmakingState(int id) : base(id) { }


        public override void EnterState(StateMachine stateMachine)
        {
            MatchmakingUiController.Instance.OnBackClick.AddListener(OnBackClickHandler);
            MatchmakingUiController.Instance.OnJoinGameClick.AddListener(OnJoinGameClickHandler);
            MatchmakingUiController.Instance.OnHostGameClick.AddListener(OnHostGameClickHandler);
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {
            MatchmakingUiController.Instance.OnBackClick.RemoveListener(OnBackClickHandler);
            MatchmakingUiController.Instance.OnJoinGameClick.RemoveListener(OnJoinGameClickHandler);
            MatchmakingUiController.Instance.OnHostGameClick.RemoveListener(OnHostGameClickHandler);
        }

        private void OnBackClickHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.AddListener(OnInAnimationCompleteMenuHandler);
            GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.MENU, true);
        }

        private void OnJoinGameClickHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.AddListener(OnInAnimationCompleteGameHandler);
            GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.GameScene, true);
            EscapeRoomApp.Instance.startAs = EStartAs.Client;
        }

        private void OnHostGameClickHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.AddListener(OnInAnimationCompleteGameHandler);
            GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.GameScene, true);
            EscapeRoomApp.Instance.startAs = EStartAs.Host;
        }

        private void OnInAnimationCompleteMenuHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.RemoveListener(OnInAnimationCompleteMenuHandler);

            Context.ChangeState(GameStates.Instance.GetMainMenuState(), GameStates.GameStatesEnum.MatchmakingState);
        }

        private void OnInAnimationCompleteGameHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.RemoveListener(OnInAnimationCompleteGameHandler);
            Context.ChangeState(GameStates.Instance.GetGameState(), GameStates.GameStatesEnum.GameState);
        }
    }
}
