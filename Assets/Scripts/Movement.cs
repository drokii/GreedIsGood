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

    public float fallMultiplier = 10f;


    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool isGrounded;
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

        // Jumping
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
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

        // Move depending on whether isGrounded, or airstrafing
        if (isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * moveSpeed * 5f * airMultiplier, ForceMode.Force);
    }

    /*
     * In order to not only correct "illegal" speed limit breaches, but also fix the weird floaty falling, we correct the player speed at specific places.
     * TODO: Refactor into different functions because of SR
     */
    private void CorrectPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVelocity.normalized * moveSpeed;
            playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
        }

        if(!isGrounded && playerRigidbody.velocity.y < 1)
        {
            playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
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
