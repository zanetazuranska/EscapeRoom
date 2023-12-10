using UnityEngine;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        private void Start()
        {
            _stateMachine.Start(GameStates.Instance.GetInitializeState());
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
