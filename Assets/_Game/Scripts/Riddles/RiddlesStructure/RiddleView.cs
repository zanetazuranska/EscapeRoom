using UnityEngine;

namespace ER.Riddle
{
    public class RiddleView : MonoBehaviour
    {
        private RiddleController _riddleController;

        public void SetRiddleController(RiddleController riddleController)
        {
            _riddleController = riddleController;
        }

        public RiddleController GetRiddleController()
        {
            return _riddleController;
        }
    }
}

