using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ER
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerNetworkController _playerNetworkController;

        CharacterController _characterController;

        private Inventory _inventory = new Inventory();

        [SerializeField] private List<Item> _items = new List<Item>();

        [SerializeField] private GameObject[] _inventoryObjects = new GameObject[2];
        private bool _isInventoryActive = false;

        public enum MovementState
        {
            Standing = 0,
            Walking = 1,
        }

        public List<MovementState> _movementStates = new List<MovementState>();

        [Header("Movement")]
        [SerializeField][Range(0f, 20f)] private float _walkSpeed = 10f;

        #region Movement variables

        private PlayerInput _playerInput;

        private bool _isStanding = false;
        private bool _isWalkingRL = false;
        private bool _isWalkingUD = false;

        private Vector2 _moveDirection;
        #endregion

        #region Physics

        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private GameObject _groundCheck;
        private float _groundDistance = 0.4f;
        private float _gravity = -9.81f;
        private Vector3 _velocity;

        #endregion

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _playerInput = new PlayerInput();
            _playerInput.Movement.Enable();
            _playerInput.Inventory.Enable();

            _playerInput.Movement.WalkRL.performed += _ => _isWalkingRL = true;
            _playerInput.Movement.WalkRL.canceled += _ => _isWalkingRL = false;

            _playerInput.Movement.WalkUD.performed += _ => _isWalkingUD = true;
            _playerInput.Movement.WalkUD.canceled += _ => _isWalkingUD = false;

            _playerInput.Inventory.ActiveDesactiv.performed += ActiveOrDesactivInventory;
        }

        private void FixedUpdate()
        {
            if (!_playerNetworkController.IsOwner)
            {
                return;
            }

            _items = _inventory.GetItems();

            if (_isInventoryActive == true) return;

            CalculateState();
            CalculateGravity();
            Move();
        }

        private void CalculateGravity()
        {
            if (GroundCheck() && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void Move()
        {
            _moveDirection.x = _playerInput.Movement.WalkRL.ReadValue<float>();
            _moveDirection.y = _playerInput.Movement.WalkUD.ReadValue<float>();

            Vector3 move = transform.right * _moveDirection.x + transform.forward * _moveDirection.y;
            _characterController.Move(move * _walkSpeed * Time.deltaTime);
        }

        private void CalculateState()
        {

            if (_isWalkingRL || _isWalkingUD)
            {
                AddStateIfPossible(MovementState.Walking);
                DeleteState(MovementState.Standing);
            }
            else
            {
                AddStateIfPossible(MovementState.Standing);
                DeleteState(MovementState.Walking);
            }
        }

        private void AddStateIfPossible(MovementState state)
        {
            if (_movementStates.Contains(state)) return;
            else _movementStates.Add(state);
        }

        private void DeleteState(MovementState state)
        {
            if (_movementStates.Contains(state))
            {
                _movementStates.Remove(state);
            }
        }

        private bool GroundCheck()
        {
            return Physics.CheckSphere(_groundCheck.transform.position, _groundDistance, _groundMask);
        }

        public Inventory GetInventory()
        {
            return _inventory;
        }

        private void ActiveOrDesactivInventory(InputAction.CallbackContext context)
        {
            if (!_playerNetworkController.IsOwner) return;

            if (_inventoryObjects[0].activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _isInventoryActive = false;

                _inventoryObjects[0].SetActive(false);
                _inventoryObjects[1].SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                _isInventoryActive = true;

                _inventoryObjects[0].SetActive(true);
                _inventoryObjects[1].SetActive(true);

                _inventory.OnInventoryChange.Invoke(_inventory.GetItems());
            }
        }

        private void OnDestroy()
        {
            _playerInput.Inventory.ActiveDesactiv.performed -= ActiveOrDesactivInventory;
        }

        public bool GetIsIventoryActive()
        {
            return _isInventoryActive;
        }

        public void PlayerCanUseInput(bool input)
        {
            _isInventoryActive = !input;
        }

        public void DesactiveInventory()
        {
            _inventoryObjects[0].SetActive(false);
            _inventoryObjects[1].SetActive(false);
        }
    }
}
