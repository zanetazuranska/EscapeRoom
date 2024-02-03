using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace ER.Riddle
{
    public class Suitcase : InteractableObject
    {
        private MeshRenderer _renderer;

        public UnityEvent OnClickEvent = new UnityEvent();

        private CameraController _cameraController;

        public bool isRiddleCorrect = false;

        private const string NEW_ITEM_TEXT = "Added new item";

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            if(!isRiddleCorrect)
            {
                OnClickEvent.Invoke();

                _cameraController = context.playerController.GetComponentInChildren<CameraController>();

                _cameraController.canMoveCamera = false;
            }
            else
            {
                context.playerController.GetInventory().Add(Item.ItemType.Button);

                StartCoroutine(DestroyGameObject(context));
            }
        }

        private IEnumerator DestroyGameObject(InteractionContext context)
        {
            StartCoroutine(context.interactionManager.ShowTextMessage(NEW_ITEM_TEXT));
            yield return new WaitForSeconds(2.0f);
            StopCoroutine(context.interactionManager.ShowTextMessage(NEW_ITEM_TEXT));

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

        public void SetCameraTrue()
        {
            _cameraController.canMoveCamera = true;
        }
    }
}

