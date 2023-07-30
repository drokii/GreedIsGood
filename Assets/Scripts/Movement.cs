using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{


    [Header("Movement")]
    // BASE VARIABLES
    public float baseMoveSpeed = 6;
    public float groundDrag = 5;
    public MovementState movementState;
    public Transform orientation;

    // JUMPING
    public float jumpForce = 7;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    bool readyToJump;
    public float fallMultiplier = 1.5f;

    // SPRINTING
    public float sprintingSpeed = 12;

    // CROUCHING
    public float crouchingSpeed = 2;

    // WALKING
    public float walkingSpeed = 4;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode walkKey = KeyCode.X;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;
    public float groundCheckOffsetDistance = 0.8f;


    float horizontalKeyboardInput;
    float verticalKeyboardInput;

    Vector3 moveDirection;

    Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        CheckIfGrounded();
        ProcessPlayerInput();
        CorrectPlayerSpeed();
        HandleDrag();
    }

    private void HandleDrag()
    {
        if (isGrounded)
            playerRigidbody.drag = groundDrag;
        else
            playerRigidbody.drag = 1;
    }
    private void CheckIfGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * groundCheckOffsetDistance, ground);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ProcessPlayerInput()
    {
        horizontalKeyboardInput = Input.GetAxisRaw("Horizontal");
        verticalKeyboardInput = Input.GetAxisRaw("Vertical");

        if (isGrounded)
        {
            if (Input.GetKeyDown(jumpKey) && readyToJump)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
                return;
            }

            if (Input.GetKey(sprintKey))
            {
                movementState = MovementState.SPRINTING;
                return;
            }

            if (Input.GetKey(walkKey))
            {
                movementState = MovementState.WALKING;
                return;
            }

            if (Input.GetKey(crouchKey))
            {
                movementState = MovementState.CROUCHING;
                return;
            }

            movementState = MovementState.RUNNING;
        }
    }

    private void MovePlayer()
    {
        // Make sure to walk in the direction you're facing.
        moveDirection = orientation.forward * verticalKeyboardInput + orientation.right * horizontalKeyboardInput;

        if (!isGrounded)
        {
            playerRigidbody.AddForce(moveDirection.normalized * baseMoveSpeed * 10f * airMultiplier, ForceMode.Force);
            return;
        }

        switch (movementState)
        {
            case MovementState.WALKING:
                playerRigidbody.AddForce(moveDirection.normalized * walkingSpeed * 10f, ForceMode.Force);
                break;
            case MovementState.SPRINTING:
                playerRigidbody.AddForce(moveDirection.normalized * sprintingSpeed * 10f, ForceMode.Force);
                break;
            case MovementState.CROUCHING:
                playerRigidbody.AddForce(moveDirection.normalized * crouchingSpeed * 10f, ForceMode.Force);
                break;
            case MovementState.RUNNING:
                playerRigidbody.AddForce(moveDirection.normalized * baseMoveSpeed * 10f, ForceMode.Force);
                break;

        }

    }

    /*
     * In order to not only correct "illegal" speed limit breaches, but also fix the weird floaty falling, we correct the player speed at specific places.
     * TODO: Refactor into different functions because of SR
     */
    private void CorrectPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        if (flatVelocity.magnitude > baseMoveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * baseMoveSpeed;
            playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
        }

        if (!isGrounded && playerRigidbody.velocity.y < 1)
        {
            playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void Jump()
    {
        // reset y velocity
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

}

public enum MovementState
{
    STILL,
    WALKING,
    RUNNING,
    SPRINTING,
    JUMPING,
    CROUCHING
}