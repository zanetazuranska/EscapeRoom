using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER.Riddle
{
    [CreateAssetMenu]
    public class MagneticCardData : RiddleData
    {
        public List<SkeletonKey> PuzzleAnswer = new List<SkeletonKey>();
        public List<SkeletonKey> PuzzleProposedAnswer = new List<SkeletonKey>();

        [System.Serializable]
        public class SkeletonKey
        {
            public int[] ValueSet = new int[4];
        }
    }
}
