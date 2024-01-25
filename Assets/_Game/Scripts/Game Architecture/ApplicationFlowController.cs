using ER.Riddle;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ER
{
    public class ApplicationFlowController : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        private List<RiddleController> _riddleControllers = new List<RiddleController>();
        public UnityEvent<RiddleController> OnAddRiddleController = new UnityEvent<RiddleController>();

        private void Start()
        {
            if(GameStates.Instance.GetInitializeState()._canEnter)
            {
                _stateMachine.Start(GameStates.Instance.GetInitializeState());
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void AddRiddleController(RiddleController riddleController)
        {
            _riddleControllers.Add(riddleController);

            OnAddRiddleController.Invoke(riddleController);
        }

        public List<RiddleController> GetRiddleControllers()
        { 
            return _riddleControllers;
        }

        public void RemoveRiddleController(RiddleController controller)
        {
            _riddleControllers.Remove(controller);
        }
    }
}
