using UnityEngine;
using UnityEngine.Events;

namespace ER.Riddle
{
    public class UpstairsDoor : InteractableObject
    {
        private MeshRenderer _renderer;

        public UnityEvent OnClickEvent = new UnityEvent();

        private CameraController _cameraController;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            Cursor.lockState = CursorLockMode.None;

            OnClickEvent.Invoke();

            _cameraController = context.playerController.GetComponentInChildren<CameraController>();

            _cameraController.canMoveCamera = false;
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
