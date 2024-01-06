using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Riddle1Paper1 = 0,
        Riddle1Paper2 = 1,
        Riddle1Paper3 = 2,
        Riddle1Paper4 = 3
    }

    public ItemType itemType;
    public string hint;
    public Sprite sprite;
}
