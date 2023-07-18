using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6;

    public float groundDrag = 5;

    public float jumpForce = 7;
    public float jumpCooldown= 0.25f;
    public float airMultiplier = 0.4f ;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public float groundCheckOffsetDistance = 0.8f;

    public Transform orientation;

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
        // ground check
        CheckIfGrounded();

        ProcessPlayerInput();
        LimitPlayerSpeed();

        // handle drag
        if (grounded)
            playerRigidbody.drag = groundDrag;
        else
            playerRigidbody.drag = 0;
    }

    private void CheckIfGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * groundCheckOffsetDistance, ground);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ProcessPlayerInput()
    {
        horizontalKeyboardInput = Input.GetAxisRaw("Horizontal");
        verticalKeyboardInput = Input.GetAxisRaw("Vertical");

        // Jumping
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // Make sure to walk in the direction you're facing.
        moveDirection = orientation.forward * verticalKeyboardInput + orientation.right * horizontalKeyboardInput;

        // Move depending on whether grounded, or airstrafing
        if (grounded)
            playerRigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            playerRigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * moveSpeed;
            playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
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
