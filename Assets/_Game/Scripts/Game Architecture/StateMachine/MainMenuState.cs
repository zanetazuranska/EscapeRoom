using UnityEngine;

namespace ER
{
    public class MainMenuState : BaseState
    {
        public MainMenuState(int id) : base(id) { }

        public override void EnterState(StateMachine stateMachine)
        {
            Debug.Log("Enter MainMenuState");

            MenuUI.Instance.OnStartClick.AddListener(OnStartClickHandler);
        }

        public override void UpdateState(StateMachine stateMachine)
        {

        }

        public override void ExitState(StateMachine stateMachine)
        {

        }

        private void OnStartClickHandler()
        {
            MenuUI.Instance.OnStartClick.RemoveListener(OnStartClickHandler);

            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.AddListener(OnInAnimationCompleteHandler);
            GameSceneManager.Instance.LoadScene(GameSceneManager.Scene.MatchmakingScene, true);
        }

        private void OnInAnimationCompleteHandler()
        {
            GameSceneManager.Instance.GetSceneFade().OnInAnimComplete.RemoveListener(OnInAnimationCompleteHandler);

            Context.ChangeState(GameStates.Instance.GetMatchmakingState(), GameStates.GameStatesEnum.GameState);
        }
    }
}