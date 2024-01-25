using System.Collections.Generic;
using UnityEngine;

namespace ER.Riddle
{
    public class RiddleData: ScriptableObject
    {
        public int id;

        public List<string> proposedAnswer = new List<string>();
    }
}

