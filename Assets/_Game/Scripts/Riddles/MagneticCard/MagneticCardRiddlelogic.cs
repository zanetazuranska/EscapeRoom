using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER.Riddle
{
    [CreateAssetMenu]
    public class MagneticCardRiddlelogic : RiddleLogic
    {
        [SerializeField] private MagneticCardData _magneticCardData;

        public override void LogicInitialize()
        {
            SetRiddleData(_magneticCardData);
        }

        public override bool CheckAnswer(RiddleData riddleData)
        {
            for(int i=0; i<_magneticCardData.PuzzleAnswer.Count; i++)
            {
                MagneticCardData.SkeletonKey skeletonKeyAnswer = _magneticCardData.PuzzleAnswer[i];
                MagneticCardData.SkeletonKey skeletonKeyProposedAnswer = _magneticCardData.PuzzleProposedAnswer[i];

                for (int j = 0; j < skeletonKeyAnswer.ValueSet.Length; i++)
                {
                    if (skeletonKeyAnswer.ValueSet[j] != skeletonKeyProposedAnswer.ValueSet[j])
                        return false;
                }
            }
            
            return true;
        }

        public override void OnRiddleCorrect()
        {
            //Maybe it will be helpfull one day
        }

        public bool CheckSkeletonKeyStep(int skeletonKeyId, int stepId)
        {
            if (_magneticCardData.PuzzleAnswer[skeletonKeyId].ValueSet[stepId] == _magneticCardData.PuzzleProposedAnswer[skeletonKeyId].ValueSet[stepId])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

