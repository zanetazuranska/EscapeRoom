using Unity.Netcode;
using UnityEngine;

namespace ER
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField] private PlayerNetworkController _playerNetworkController;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameObject _camera;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _mouseSensivity = 100f;

        private PlayerInput _playerInput;

        private float _mouseX;
        private float _mouseY;

        private float _rotation = 0f;

        public override void OnNetworkSpawn()
        {
            if (this.IsOwner)
            {
                _camera.SetActive(true);
            }

            _playerInput = new PlayerInput();
            _playerInput.CameraController.Enable();
        }

        private void Update()
        {
            if (_playerController.GetIsIventoryActive() == true) return;

            SetMouseDelta();
            SetCamera();
        }

        private void SetMouseDelta()
        {
            _mouseX = _playerInput.CameraController.MouseX.ReadValue<float>() * _mouseSensivity * Time.deltaTime;

            _mouseY = _playerInput.CameraController.MouseY.ReadValue<float>() * _mouseSensivity * Time.deltaTime;
        }

        private void SetCamera()
        {
            _playerTransform.Rotate(Vector3.up * _mouseX);

            _rotation -= _mouseY;
            _rotation = Mathf.Clamp(_rotation, -40f, 40f);
            transform.localRotation = Quaternion.Euler(_rotation, 0f, 0f);
        }
    }

}