
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

    private float transitionSpeed = 15f;

    void Start()
    {
        changePerspectiveBasedOnMovement();
    }
    void Update()
    {
        changePerspectiveBasedOnMovement();
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
        transform.position = Vector3.Lerp(transform.position, newCameraTransform.position, transitionSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newCameraTransform.rotation, transitionSpeed * Time.deltaTime);
    }
}
