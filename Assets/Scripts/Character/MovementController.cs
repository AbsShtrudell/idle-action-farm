using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [Zenject.Inject]private Joystick joystick;
    [Zenject.Inject]private PlayerController playerController;

    [SerializeField, Min(0)] private float movementSpeed = 1;
    [SerializeField, Range(0, 1)] private float fieldMovemenMultiplier = 0.6f;
    [SerializeField, Min(0)] private float rotationSpeed = 10000;
    [SerializeField, Range(0f, 1f)] private float movementBlockPrecent = 0.3f;

    private CharacterController characterController;

    private Vector3 movementDirection;

    public float MaxSpeed
    { get; private set; }

    public float CurrentSpeedPercent
    { get { return movementDirection.magnitude * MaxSpeed / movementSpeed; } }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        MaxSpeed = movementSpeed;
    }

    private void OnEnable()
    {
        playerController.onWorkStateChanged += HandleMaxSpeed;
    }

    private void OnDisable()
    {
        playerController.onWorkStateChanged -= HandleMaxSpeed;
    }

    private void Update()
    {
        if (UpdateMovementDirection() != Vector3.zero)
        {
            UpdateMovement();
            UpdateRotation();
        }
    }

    private void UpdateMovement()
    {
        if (CurrentSpeedPercent <= movementBlockPrecent) return;

        characterController.Move(movementDirection * MaxSpeed * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private Vector3 UpdateMovementDirection()
    {
        return movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }

    private void HandleMaxSpeed(bool isWorking)
    {
        MaxSpeed = isWorking ? fieldMovemenMultiplier * movementSpeed : movementSpeed;
    }
}
