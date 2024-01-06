using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot: MonoBehaviour
{
    [SerializeField] private Image _image;
    private Item.ItemType _itemType;
    private bool _isActive;

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
}
