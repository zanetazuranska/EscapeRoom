using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class KithenTable : InteractableObject
    {
        [SerializeField] private Animator _animator;
        private MeshRenderer _renderer;
        private const string NO_AXE = "The hinges in the cabinet are so old that they are stuck. A sharp tool would help.";

        private const string ANIMATOR_BOOL = "CanOpen";

        [SerializeField] private KithenTableNetworking _networkObject; 

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            if (context.playerController.GetInventory().GetItems().Contains(ItemRegister.Instance.GetItem(Item.ItemType.Axe)))
            {
                _networkObject.SetIsOpen(true);

                _animator.SetBool(ANIMATOR_BOOL, true);
                context.playerController.GetInventory().Remove(Item.ItemType.Axe);
            }
            else
            {
                if(!_networkObject.GetIsOpen())
                {
                    StartCoroutine(context.interactionManager.ShowTextMessage(NO_AXE));
                }
            }      
        }

        public override void OnHover()
        {
            if (!_networkObject.GetIsOpen())
            {
                _renderer.materials[1].SetFloat("_Scale", 1.03f);
            } 
        }

        public override void OnUnHover()
        {
            _renderer.materials[1].SetFloat("_Scale", 0f);
        }
    }
}

