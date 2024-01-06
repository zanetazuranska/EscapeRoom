using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot: MonoBehaviour
{
    [SerializeField] private Image _image;
    private Item.ItemType _itemType;
    private bool _isActive;

    [SerializeField] private GameObject _inventoryEnlargedItem;
    [SerializeField] private Image _itemSpriteHolder;

    [SerializeField] private GameObject _hint;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnSlotClick);
    }

    public void ActiveSlot()
    {
        _image.enabled = true;
        _isActive = true;
    }

    public void DeactiveSlot()
    {
        _image.enabled = false;
        _image.sprite = null;
        _isActive = false;
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetItemType(Item.ItemType type)
    {
        _itemType = type;
    }

    private void OnSlotClick()
    {
        if(_isActive)
        {
            _inventoryEnlargedItem.SetActive(true);
            _itemSpriteHolder.sprite = ItemRegister.Instance.GetItem(_itemType).sprite;
        }
    }

    public void OnSlotHover()
    {
        if(_isActive)
        {
            _hint.SetActive(true);
            _text.text = ItemRegister.Instance.GetItem(_itemType).hint;
        }
    }

    public void OnSlotUnHover()
    {
        if (_isActive) 
        {
            _hint.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GetComponent<Button>().onClick.RemoveListener(OnSlotClick);
    }
}
