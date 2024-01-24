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

        private const string ANIMATION_VARIABLE = "CanOpen";

        [SerializeField] private List<GameObject> _players = new List<GameObject>();

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

                foreach (var player in _players)
                {
                    Debug.Log("SetPos" + player.GetComponent<PlayerNetworkController>().IsHost);

                    player.transform.position = new Vector3(20.0f, 7.57f, -3.1f);
                    player.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
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
            _players.Add(player);
        }
    }
}
