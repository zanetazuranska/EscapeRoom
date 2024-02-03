using ER;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class LaserPanel : InteractableObject
    {
        private MeshRenderer _renderer;
        private const string NO_KNOB = "The button is missing";

        [SerializeField] private LaserPanelNetworkObject _laserPanelNetworkObject;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            List<Item> items = context.playerController.GetInventory().GetItems();
            Item button = ItemRegister.Instance.GetItem(Item.ItemType.Button);

            if(items.Contains(button))
            {
                _laserPanelNetworkObject.SwitchButtons();
            }
            else
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(NO_KNOB));
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


