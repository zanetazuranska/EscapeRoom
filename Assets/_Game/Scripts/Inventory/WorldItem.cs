using NUnit.Framework.Internal.Execution;
using UnityEngine;
using System.Collections;

namespace ER
{
    public class WorldItem : InteractableObject
    {
        [SerializeField] private Item.ItemType _itemType;
        [SerializeField] private MeshRenderer _renderer;

        public override void OnClick(InteractionContext context)
        {
            context.playerController.GetInventory().Add(_itemType);
            Destroy(this.gameObject);
        }

        public override void OnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 1.03f);
        }

        public override void OnUnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }

        public Item.ItemType GetItemType()
        {
            return _itemType;
        }
    }

}