using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHander : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    Move inputActions;
    private CameraHandel _cameraHandel;

    private Vector2 movementInput;
    private Vector2 cameraInput;

    private void Awake()
    {
        _cameraHandel = CameraHandel.singleton;
    }

    private void Start()
    {
        _cameraHandel = CameraHandel.singleton;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (_cameraHandel != null)
        {
            _cameraHandel.Followtarget(delta);
            _cameraHandel.HandleCamerarotion(delta,mouseX,mouseY);
        }
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new Move();
            inputActions.Player.Movement.performed += inputActions =>
                movementInput = inputActions.ReadValue<Vector2>();
            inputActions.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
        
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
    }
    public void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01((Mathf.Abs(horizontal) + Mathf.Abs(vertical)));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}
