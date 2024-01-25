using UnityEngine;

namespace ER.Riddle
{
    [System.Serializable]
    public abstract class RiddleLogic : ScriptableObject
    {
        [SerializeField] private RiddleType.ERiddleType _riddleType;
        [SerializeField] private RiddleData _riddleData;

        public abstract bool CheckAnswer(RiddleData riddleData);
        public abstract void OnRiddleCorrect();
        public abstract void LogicInitialize();

        public RiddleData GetRiddleData() 
        { 
            return _riddleData; 
        }

        public RiddleType.ERiddleType GetRiddleType()
        {
            return _riddleType;
        }

        public void SetRiddleData(RiddleData riddleData)
        {
            _riddleData = riddleData;
        }
    }

}