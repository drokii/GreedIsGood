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
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryMenu.activeInHierarchy)
            {
                inventoryMenu.SetActive(true);
                playerCamera.enabled = false;
                Cursor.visible = true;
                Debug.Log(Cursor.visible);
            }
            else
            {
                inventoryMenu.SetActive(false);
                playerCamera.enabled = true;
                Cursor.visible = false;
                Debug.Log(Cursor.visible);
            }
        }
        
    }
}
