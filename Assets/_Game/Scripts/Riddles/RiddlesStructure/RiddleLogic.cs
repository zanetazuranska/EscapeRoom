using UnityEngine;

namespace ER.Riddle
{
    public abstract class RiddleLogic : ScriptableObject
    {
        [SerializeField] private RiddleType.ERiddleType _riddleType;
        private RiddleData _riddleData;

        public abstract bool CheckAnswer(RiddleData riddleData);

        public RiddleData GetRiddleData() 
        { 
            return _riddleData; 
        }

        public RiddleType.ERiddleType GetRiddleType()
        {
            return _riddleType;
        }
    }

}