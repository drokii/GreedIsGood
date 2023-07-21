using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject inventoryMenu;
    public PlayerCamera playerCamera;

    void Start()
    {
        inventoryMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryMenu.activeInHierarchy)
            {
                inventoryMenu.SetActive(true);
                inventoryMenu.GetComponent<InventoryMenu>().loadItemsFromPlayerInventory();
                playerCamera.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                inventoryMenu.SetActive(false);
                playerCamera.enabled = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        
    }
}
