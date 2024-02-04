using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.InputSystem;
using ER;

public class MessageController : NetworkBehaviour
{
    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _textGameObject;
    [SerializeField] private GameObject _inputFieldGameObject;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private CameraController _cameraController;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Message.Enable();

        _playerInput.Message.Active.performed += Active;
        _playerInput.Message.Send.performed += Send;
    }

    private void Active(InputAction.CallbackContext context)
    {
        if(IsOwner)
        {
            if(_message.activeSelf == false)
            {
                _inputField.text = "";
                _message.SetActive(true);
                _inputFieldGameObject.SetActive(true);

                Cursor.lockState = CursorLockMode.None;

                _cameraController.canMoveCamera = false;
            }
            else
            {
                _message.SetActive(false);
                _inputFieldGameObject.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;

                _cameraController.canMoveCamera = true;
            }
            
        }
    }

    private void Send(InputAction.CallbackContext context)
    {
        _inputFieldGameObject.SetActive(false);
        _message.SetActive(false);

        if (IsHost)
        {
            UpdateMessageTextClientRpc(_inputField.text);
        }
        else
        {
            UpdateMessageTextServerRpc(_inputField.text);
        }
    }

    [ClientRpc]
    private void UpdateMessageTextClientRpc(string message)
    {
        if(!IsOwner)
        {
            _inputFieldGameObject.SetActive(false);
            StartCoroutine(ShowMessage(message));
        }
    }

    [ServerRpc (RequireOwnership = false)]
    private void UpdateMessageTextServerRpc(string message)
    {
        UpdateMessageTextClientRpc(message);
    }

    private IEnumerator ShowMessage(string message)
    {
        if (IsOwner)
        {
            StopCoroutine("ShowMessage");
        }

        _text.text = message;
        _message.SetActive(true);
        _textGameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        _message.SetActive(false);
        _textGameObject.SetActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _playerInput.Message.Active.performed -= Active;
        _playerInput.Message.Send.performed -= Send;
    }
}
