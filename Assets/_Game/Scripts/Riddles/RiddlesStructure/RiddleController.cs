using UnityEngine;

namespace ER.Riddle
{
    public class RiddleController : MonoBehaviour
    {
        [SerializeField] private RiddleLogic _riddleLogic;

        public bool IsAnswerCorrect(RiddleData riddleData)
        {
            return _riddleLogic.CheckAnswer(riddleData);
        }

        public RiddleData GetRiddleData()
        {
            return _riddleLogic.GetRiddleData();
        }
    }
}

