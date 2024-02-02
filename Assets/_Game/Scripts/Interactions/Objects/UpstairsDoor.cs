using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace ER
{
    public class UpstairsDoor : InteractableObject
    {
        private MeshRenderer _renderer;

        [SerializeField] private GameObject _upstairsRiddleUI;
        [SerializeField] private Button _exit;

        private CameraController _cameraController;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void OnClick(InteractionContext context)
        {
            Cursor.lockState = CursorLockMode.None;
            _upstairsRiddleUI.SetActive(true);

            _cameraController = context.playerController.GetComponent<CameraController>();

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

        public void OnExitClick()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _upstairsRiddleUI.SetActive(false);
        }

        public override void OnDestroy()
        {
            _exit.onClick.RemoveListener(OnExitClick);
        }

        public void SetRiddleUI(GameObject upstairsRiddleUI)
        {
            _upstairsRiddleUI = upstairsRiddleUI;
        }

        public void SetExitButton(Button exit)
        {
            _exit = exit;
            _exit.onClick.AddListener(OnExitClick);
        }
    }
}

