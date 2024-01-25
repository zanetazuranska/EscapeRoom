using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER.Riddle
{
    [CreateAssetMenu]
    public class BarrelLockerData : RiddleData
    {
        public List<string> PuzzleAnswer = new List<string>();
        public List<BarelData> Barrels = new List<BarelData>();

        [System.Serializable]
        public class BarelData
        {
            public string[] ValueSet = new string[10];
        }
    }
}

