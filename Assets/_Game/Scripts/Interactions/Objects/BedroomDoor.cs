using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class BedroomDoor : InteractableObject
    {
        private MeshRenderer _renderer;

        [SerializeField] private BedroomDoorNetworkObject _bedroomDoorNetworkObject;
        private const string NO_CARD = "You need a card";

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            List<Item> items = context.playerController.GetInventory().GetItems();
            Item card = ItemRegister.Instance.GetItem(Item.ItemType.MagneticCard);

            if(items.Contains(card))
            {
                context.playerController.GetInventory().Remove(Item.ItemType.MagneticCard);
                _bedroomDoorNetworkObject.DestroyDoor();
            }
            else
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(NO_CARD));
            }
        }

        public override void OnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 1.03f);
        }

        public override void OnUnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }
    }
}

