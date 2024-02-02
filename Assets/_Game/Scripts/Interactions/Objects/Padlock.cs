using ER.Riddle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ER
{
    public class Padlock : InteractableObject
    {
        [SerializeField] private Animator _animator;
        private MeshRenderer _renderer;
        private const string PADLOCK = "Have you ever experienced a break-in?";

        public UnityEvent OnClickEvent = new UnityEvent();

        [SerializeField] private RiddleController _riddleController;

        [SerializeField] private MetalBoxNetworkObject _networkObject;

        private const string ANIMATOR_BOOL = "CanOpen";

        private CameraController _cameraController;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();

            _riddleController.OnAnswerCorrectEvent.AddListener(OnRiddleCorrect);
        }

        public override void OnClick(InteractionContext context)
        {
            if (_networkObject.GetIsOpen())
            {
                return;
            }

            List<Item> items = context.playerController.GetInventory().GetItems();
            Item spoon = ItemRegister.Instance.GetItem(Item.ItemType.Spoon);
            Item wire = ItemRegister.Instance.GetItem(Item.ItemType.Wire);

            if (items.Contains(spoon) && items.Contains(wire))
            {
                OnClickEvent.Invoke();

                _cameraController = context.playerController.GetComponentInChildren<CameraController>();

                _cameraController.canMoveCamera = false;
            }
            else
            {
                StartCoroutine(context.interactionManager.ShowTextMessage(PADLOCK));
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

        private void OnRiddleCorrect()
        {
            _networkObject.SetIsOpen(true);

            _animator.SetBool(ANIMATOR_BOOL, true);

            _riddleController.OnAnswerCorrectEvent.RemoveListener(OnRiddleCorrect);
        }

        public void SetCameraTrue()
        {
            _cameraController.canMoveCamera = true;
        }
    }
}

