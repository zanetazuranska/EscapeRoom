using ER;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegister : MonoBehaviour
{
    public static ItemRegister Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private List<Item> _items = new List<Item>();

    /// <summary>  
    /// If there is no such item, it returns null 
    /// </summary> 
    public Item GetItem(Item.ItemType _itemType)
    {
        foreach (Item item in _items)
        {
            if(item.itemType == _itemType)
            {
                return item;
            }
        }

        return null;
    }
}
