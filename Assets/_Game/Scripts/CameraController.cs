using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : NetworkBehaviour
{
    [SerializeField] private PlayerNetworkController _playerNetworkController;
    [SerializeField] private GameObject _camera;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _mouseSensivity = 100f;

    private PlayerInput _playerInput;

    private float _mouseX;
    private float _mouseY;

    private float _rotation = 0f;

    private void Start()
    {

    }

    public override void OnNetworkSpawn()
    {
        if(this.IsOwner)
        {
            _camera.SetActive(true);
        }

        _playerInput = new PlayerInput();
        _playerInput.CameraController.Enable();
    }

    void Update()
    {
        //if (this.IsClient)
        //{
         //   Debug.Log("NotOwner");
         //   return;
        //}

        SetMouseDelta();
        SetCamera();
    }

    void SetMouseDelta()
    {
        _mouseX = _playerInput.CameraController.MouseX.ReadValue<float>() * _mouseSensivity * Time.deltaTime;

        _mouseY = _playerInput.CameraController.MouseY.ReadValue<float>() * _mouseSensivity * Time.deltaTime;
    }
    
    void SetCamera()
    {
        _playerTransform.Rotate(Vector3.up * _mouseX);

        _rotation -= _mouseY;
        _rotation=Mathf.Clamp(_rotation, -30f, 30f);
        transform.localRotation= Quaternion.Euler(_rotation, 0f, 0f);
    }
}
