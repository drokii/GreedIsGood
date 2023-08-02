
using TMPro;
using UnityEngine;
/*
    In order to mitigate buggy camera movement, we keep the camera out of the player object, but sync
    it's position with it instead.
*/
public class CameraMover : MonoBehaviour
{
    public Transform normalCameraTransform;
    public Transform crouchingCameraTransform;
    public Movement movement;

    private float transitionSpeed = 10.0f;
    private MovementState currentMovementState;
    private Transform currentCameraTransform;

    void Start()
    {
        changePerspectiveBasedOnMovement();
    }
    void Update()
    {

        transform.position = currentCameraTransform.position;

        if (currentMovementState != movement.movementState) { changePerspectiveBasedOnMovement(); }

    }

    private void changePerspectiveBasedOnMovement()
    {
        if (movement.movementState == MovementState.CROUCHING)
        {
            setCameraPerspective(crouchingCameraTransform);
        }
        else
        {
            setCameraPerspective(normalCameraTransform);
        }
    }

    private void setCameraPerspective(Transform newCameraTransform)
    {
        currentCameraTransform = newCameraTransform;
        transform.position = Vector3.Lerp(transform.position, normalCameraTransform.position, transitionSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, normalCameraTransform.rotation, transitionSpeed * Time.deltaTime);
    }
}
