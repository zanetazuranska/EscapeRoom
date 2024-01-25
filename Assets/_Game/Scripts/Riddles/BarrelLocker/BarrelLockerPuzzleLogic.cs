using UnityEngine;

namespace ER.Riddle
{
    [CreateAssetMenu]
    public class BarrelLockerPuzzleLogic : RiddleLogic
    {
        [SerializeField] private BarrelLockerData _barrelLockerData;

        public override void LogicInitialize()
        {
            SetRiddleData(_barrelLockerData);
        }

        public override bool CheckAnswer(RiddleData riddleData)
        {
            for(int i = 0; i < _barrelLockerData.PuzzleAnswer.Count; i++)
            {
                if (_barrelLockerData.PuzzleAnswer[i] != riddleData.proposedAnswer[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override void OnRiddleCorrect()
        {
            //Maybe it will be helpfull one day
        }
    }
}
