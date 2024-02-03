using UnityEngine;

namespace ER
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public enum ItemType
        {
            Riddle1Paper1 = 0,
            Riddle1Paper2 = 1,
            Riddle1Paper3 = 2,
            Riddle1Paper4 = 3,
            DoorKey = 4,
            Axe = 5,
            Spoon = 6,
            Wire = 7,
            MagneticCard = 8,
            Riddle2Paper1 = 9,
            Riddle2Paper2 = 10,
            Riddle2Paper3 = 11,
            Riddle2Paper4 = 12,
            Riddle2Paper5 = 13,
        }

        public ItemType itemType;
        public string hint;
        public Sprite sprite;
    }

}