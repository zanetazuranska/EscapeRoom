using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class Padlock : InteractableObject
    {
        private MeshRenderer _renderer;
        private const string PADLOCK = "Have you ever experienced a break-in?";

        public UnityEvent OnClickEvent = new UnityEvent();

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            List<Item> items = context.playerController.GetInventory().GetItems();
            Item spoon = ItemRegister.Instance.GetItem(Item.ItemType.Spoon);
            Item wire = ItemRegister.Instance.GetItem(Item.ItemType.Wire);

            if (items.Contains(spoon) && items.Contains(wire))
            {
                OnClickEvent.Invoke();
            }
            else
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(PADLOCK));
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

