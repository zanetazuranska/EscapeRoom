using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerNetworkController _playerNetworkController;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _mouseSensivity = 100f;

    private PlayerInput _playerInput;

    private float _mouseX;
    private float _mouseY;

    private float _rotation = 0f;

    private void Start()
    {
        _playerInput = new PlayerInput();
        _playerInput.CameraController.Enable();

    }
    void Update()
    {
        if (!_playerNetworkController.IsOwner) return;

        _mouseX = _playerInput.CameraController.MouseX.ReadValue<float>() * _mouseSensivity * Time.deltaTime;

        _mouseY = _playerInput.CameraController.MouseY.ReadValue<float>() * _mouseSensivity * Time.deltaTime;

        SetCamera();
    }
    
    void SetCamera()
    {
        _playerTransform.Rotate(Vector3.up * _mouseX);

        _rotation -= _mouseY;
        _rotation=Mathf.Clamp(_rotation, -30f, 30f);
        transform.localRotation= Quaternion.Euler(_rotation, 0f, 0f);
    }
}
