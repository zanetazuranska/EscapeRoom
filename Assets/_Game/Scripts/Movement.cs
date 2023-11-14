using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    CharacterController _characterController;
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

        _playerInput.Movement.WalkRL.performed += _ => _isWalkingRL = true;
        _playerInput.Movement.WalkRL.canceled += _ => _isWalkingRL = false;

        _playerInput.Movement.WalkUD.performed += _ => _isWalkingUD = true;
        _playerInput.Movement.WalkUD.canceled += _ => _isWalkingUD = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateState();

        if (GroundCheck() && _velocity.y<0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        Move();
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
        if(_movementStates.Contains(state))
        {
            _movementStates.Remove(state);
        }
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(_groundCheck.transform.position, _groundDistance, _groundMask);
    }
}
