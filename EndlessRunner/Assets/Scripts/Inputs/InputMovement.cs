using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputActionMap playerMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private bool jump;
    private Vector3 movement = Vector3.zero;
    public bool Jump { get => jump; }
    public Vector3 Movement { get => movement; }

    private void Start()
    {
        playerMap = inputActionAsset.FindActionMap("Player");

        moveAction = playerMap.FindAction("Movement");
        jumpAction = playerMap.FindAction("Jump");

        inputActionAsset.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;
    }

    private void OnDestroy()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJump;

        inputActionAsset.Disable();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        jump = obj.ReadValueAsButton();
    }
    private void OnMove(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }
}
