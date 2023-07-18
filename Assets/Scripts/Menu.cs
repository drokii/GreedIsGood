using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public PlayerCamera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryMenu.activeInHierarchy)
            {
                inventoryMenu.SetActive(true);
                playerCamera.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                inventoryMenu.SetActive(false);
                playerCamera.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
}
