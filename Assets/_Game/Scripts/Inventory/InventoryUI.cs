using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ER
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> _slots = new List<InventorySlot>();

        [SerializeField] private Button _arrowLeft;
        [SerializeField] private Button _arrowRight;

        [SerializeField] private Sprite[] _activearrows = new Sprite[2];
        [SerializeField] private Sprite[] _desactivearrows = new Sprite[2];

        private int _startOfChecking = 0;
        private int _endOfChecking = 0;

        private int _inventorySize;
        private List<Item> _currentItemList = new List<Item>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<InventorySlot>() != null)
                {
                    _slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
                }
            }

            if (GetComponentInParent<PlayerController>() != null)
            {
                GetComponentInParent<PlayerController>().GetInventory().OnInventoryChange.AddListener(UpdateInventoryUI);
            }

            _arrowLeft.onClick.AddListener(OnArrowLeftClick);
            _arrowRight.onClick.AddListener(OnArrowRightClick);

            _arrowLeft.GetComponent<Image>().sprite = _desactivearrows[0];
            _arrowRight.GetComponent<Image>().sprite= _desactivearrows[1];
        }

        private void UpdateInventoryUI(List<Item> items)
        {
            _inventorySize = items.Count;
            _currentItemList = items;

            if (_inventorySize > 0)
            {
                if (_inventorySize > 10)
                {
                    _endOfChecking = 10 + _startOfChecking;
                }
                else
                {
                    _endOfChecking = _inventorySize;
                }

                int j = 0;

                if(_startOfChecking == 0)
                {
                    _arrowLeft.GetComponent<Image>().sprite = _desactivearrows[0];
                }
                else
                {
                    _arrowLeft.GetComponent<Image>().sprite = _activearrows[0];
                }

                if (_inventorySize > 10 && _endOfChecking < _inventorySize)
                {
                    _arrowRight.GetComponent<Image>().sprite = _activearrows[1];
                }
                else
                {
                    _arrowRight.GetComponent<Image>().sprite = _desactivearrows[1];
                }

                for (int i = _startOfChecking; i < _endOfChecking; i++, j++)
                {
                    InventorySlot slot = _slots[j];
                    slot.SetItemType(items[i].itemType);
                    slot.SetImage(ItemRegister.Instance.GetItem(items[i].itemType).sprite);
                    slot.ActiveSlot();
                }

                if (_endOfChecking < _slots.Count)
                {
                    for (int i = _endOfChecking; i < _slots.Count; i++)
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

        private void OnArrowLeftClick()
        {
            if (_startOfChecking > 0)
            {
                _startOfChecking--;

                UpdateInventoryUI(_currentItemList);
            }
        }

        private void OnArrowRightClick()
        {
            if (_inventorySize > 10 && _endOfChecking < _inventorySize)
            {
                _startOfChecking++;

                UpdateInventoryUI(_currentItemList);
            }

        }

        private void OnDestroy()
        {
            if (GetComponentInParent<PlayerController>() != null)
            {
                GetComponentInParent<PlayerController>().GetInventory().OnInventoryChange.RemoveListener(UpdateInventoryUI);
            }

            _arrowLeft.onClick.RemoveListener(OnArrowLeftClick);
            _arrowRight.onClick.RemoveListener(OnArrowRightClick);
        }
    }

}