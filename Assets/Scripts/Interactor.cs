using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    public Ray interactionRaycast;

    public RaycastHit interactionRaycastHit;

    public Camera playerCamera;

    public float pickupDistance = 2f;

    private Inventory inventory;

    private void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * 10, Color.red);
        ProcessPlayerInput();
    }

    private void ProcessPlayerInput()
    {
        if (Input.GetKey(KeyCode.E))
        {   
            interactionRaycast = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

            Physics.Raycast(interactionRaycast, out interactionRaycastHit, pickupDistance);
         
            if (interactionRaycastHit.collider != null)
            {
                GameObject hitObject = interactionRaycastHit.collider.gameObject;

                if (hitObject.CompareTag("Item"))
                {
                    inventory.PickUp(hitObject);
                    Debug.Log("Item is hit");
                }

                if (hitObject.CompareTag("Interactable"))
                {
                    Debug.Log("Interactable is hit");
                }

            }

        }
    }
}
