using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class Inventory
    {
        [SerializeField] private List<Item> _items = new List<Item>();

        public UnityEvent<List<Item>> OnInventoryChange = new UnityEvent<List<Item>>();

        public bool Add(Item.ItemType _itemType)
        {
            Item item = ItemRegister.Instance.GetItem(_itemType);

            if (item == null)
            {
                return false;
            }
            else
            {
                _items.Add(item);
                OnInventoryChange.Invoke(_items);
                return true;
            }
        }

        public bool AddItem(Item item)
        {
            _items.Add(item);
            OnInventoryChange.Invoke(_items);
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
                OnInventoryChange.Invoke(_items);
                return true;
            }

            return false;
        }

        public bool Remove(Item.ItemType _itemType)
        {
            Item item = ItemRegister.Instance.GetItem(_itemType);

            if (item != null && _items.Contains(item))
            {
                _items.Remove(item);
                OnInventoryChange.Invoke(_items);
                return true;
            }

            return false;
        }

        public List<Item> GetItems()
        {
            return _items;
        }
    }
}


