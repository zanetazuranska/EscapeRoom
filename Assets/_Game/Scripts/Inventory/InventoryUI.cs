using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> _slots = new List<InventorySlot>();

    private int _startOfChecking = 0;
    private int _endOfChecking = 0;

    private void Awake()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<InventorySlot>() != null )
            {
                _slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
            }
        }

        if(GetComponentInParent<PlayerController>() != null )
        {
            GetComponentInParent<PlayerController>().GetInventory().OnInventoryChange.AddListener(UpdateInventoryUI);
        } 
    }

    private void UpdateInventoryUI(List<Item> items)
    {
        int itemsSize = items.Count;

        if(itemsSize > 0 )
        {
            if (itemsSize > 11) 
            { 
                _endOfChecking = 10;
            }
            else
            {
                _endOfChecking = itemsSize;
            }

            for(_startOfChecking = 0; _startOfChecking<_endOfChecking;  _startOfChecking++)
            {
                InventorySlot slot = _slots[_startOfChecking];
                slot.SetItemType(items[_startOfChecking].itemType);
                slot.SetImage(ItemRegister.Instance.GetItem(items[_startOfChecking].itemType).sprite);
                slot.ActiveSlot();
            }

            if(_endOfChecking < _slots.Count)
            {
                for(int i = _endOfChecking; i < _slots.Count; i++)
                {
                    _slots[i].DeactiveSlot();
                }
            }
        }
        else
        {
            foreach (InventorySlot slot in _slots)
            {
                slot.DeactiveSlot();
            }
        }
    }

    private void OnDestroy()
    {
        if (GetComponentInParent<PlayerController>() != null)
        {
            GetComponentInParent<PlayerController>().GetInventory().OnInventoryChange.RemoveListener(UpdateInventoryUI);
        }
    }
}
