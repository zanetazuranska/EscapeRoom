using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ER
{
    public class Locker : InteractableObject
    {
        [SerializeField] private Animator _animator;
        private MeshRenderer _renderer;

        private const string ANIMATOR_BOOL = "CanOpen";
        private const string NO_KEY = "The locker is closed";

        [SerializeField] private LockerNetworkObject _networkObject;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            List<Item> items = context.playerController.GetInventory().GetItems();
            Item rackKey = ItemRegister.Instance.GetItem(Item.ItemType.RackKey);

            if(items.Contains(rackKey))
            {
                _networkObject.SetIsOpen(true);

                _animator.SetBool(ANIMATOR_BOOL, true);

                context.playerController.GetInventory().Remove(Item.ItemType.RackKey);
            }
            else if(!_networkObject.GetIsOpen())
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(NO_KEY));
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

