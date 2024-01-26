using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class GarageDoor : InteractableObject
    {
        public static GarageDoor Instance { get; private set; }

        private MeshRenderer _renderer;

        [SerializeField] private Animator _doorAnimator;
        [SerializeField] private Animator _handleAnimator;

        [SerializeField] private PlayerNetworkSetPosition _playerNetworkSetPosition;

        private const string ANIMATION_VARIABLE = "CanOpen";

        private const string DOOR = "The door is closed... How to get out?";

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public override void OnClick(InteractionContext context)
        {
            if (context.playerController.GetInventory().GetItems().Contains(ItemRegister.Instance.GetItem(Item.ItemType.DoorKey)))
            {
                context.playerController.PlayerCanUseInput(false);

                _playerNetworkSetPosition.SetPlayerNetworkController(context.playerController.gameObject.GetComponent<PlayerNetworkController>());

                _playerNetworkSetPosition.SetPositionClientRpc();

                if(context.playerController.gameObject.GetComponent<PlayerNetworkController>().IsClient)
                {
                    _playerNetworkSetPosition.SetPositionServerRpc();
                }
                
                _doorAnimator.SetBool(ANIMATION_VARIABLE, true);
                _handleAnimator.SetBool(ANIMATION_VARIABLE, true);
            }
            else
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(DOOR));
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

        public void RegisterPlayer(GameObject player)
        {
            _playerNetworkSetPosition.RegisterPlayer(player);
        }
    }
}
