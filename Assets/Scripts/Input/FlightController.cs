using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

[RequireComponent(typeof(CharacterController))]
public class XRFlightAndLocomotion : MonoBehaviour
{
    [Header("References")]
    public XROrigin xrOrigin;
    public ContinuousMoveProvider moveProvider;

    [Header("Input Actions")]
    public InputActionReference flyUp;
    public InputActionReference flyDown;
    public InputActionReference moveAction;

    [Header("Movement Settings")]
    public float horizontalSpeed = 2f;
    public float flightSpeed = 2f;
    public float gravity = 9.81f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private CharacterController characterController;
    private float verticalVelocity = 0f;

    private enum FlightState { Grounded, Flying, Falling }
    private FlightState flightState = FlightState.Grounded;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        flyUp.action.Enable();
        flyDown.action.Enable();
        moveAction.action.Enable();
    }

    void OnDisable()
    {
        flyUp.action.Disable();
        flyDown.action.Disable();
        moveAction.action.Disable();
    }

    void Update()
    {
        bool isGrounded = CheckGrounded();

        UpdateFlightState(isGrounded);
        HandleMovement(isGrounded);
    }

    bool CheckGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.05f;
        return Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);
    }

    void UpdateFlightState(bool isGrounded)
    {
        switch (flightState)
        {
            case FlightState.Grounded:
                
                if (flyUp.action.IsPressed() || flyDown.action.IsPressed())
                {
                    EnableFlight();
                }
                break;

            case FlightState.Flying:
                
                if (isGrounded)
                {
                    DisableFlight();
                }
                break;

            case FlightState.Falling:
              
                if (flyUp.action.IsPressed())
                {
                    EnableFlight();
                }
              
                else if (isGrounded)
                {
                    flightState = FlightState.Grounded;
                }
                break;
        }

        // Determina caduta naturale
        if (flightState == FlightState.Grounded && !isGrounded)
        {
            flightState = FlightState.Falling;
        }
    }

    void HandleMovement(bool isGrounded)
    {
        Vector2 stick = moveAction.action.ReadValue<Vector2>();
        Vector3 move = Vector3.zero;

        Transform head = xrOrigin.Camera.transform;
        Vector3 forward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
        Vector3 right = new Vector3(head.right.x, 0, head.right.z).normalized;

        move += (forward * stick.y + right * stick.x) * horizontalSpeed;

        // Gestione movimento verticale
        switch (flightState)
        {
            case FlightState.Grounded:
                verticalVelocity = 0f;
                break;

            case FlightState.Flying:
                float verticalInput = 0f;
                if (flyUp.action.IsPressed()) verticalInput += 1f;
                if (flyDown.action.IsPressed()) verticalInput -= 1f;
                verticalVelocity = verticalInput * flightSpeed;
                break;

            case FlightState.Falling:
                verticalVelocity -= gravity * Time.deltaTime;
                break;
        }

        move += Vector3.up * verticalVelocity;
        characterController.Move(move * Time.deltaTime);
    }

    void EnableFlight()
    {
        flightState = FlightState.Flying;
        moveProvider.enableFly = true;
    }

    void DisableFlight()
    {
        flightState = FlightState.Grounded;
        verticalVelocity = 0f;
        moveProvider.enableFly = false;
    }
}