using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    In order to mitigate buggy camera movement, we keep the camera out of the player object, but sync
    it's position with it instead.
*/
public class CameraMover : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        transform.position = cameraTransform.position;
    }
}
