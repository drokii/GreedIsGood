using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<InventorySlot> inventorySlots;

    public GameObject inventoryIconPrefab;

    void Start()
    {
        loadItemsFromPlayerInventory();
    }

    public void loadItemsFromPlayerInventory()
    {
        foreach (Item item in playerInventory.Items)
        {
            foreach(InventorySlot slot in inventorySlots)
            {
                if (slot.CanAcceptItem(item))
                {
                    FillInventorySlotWithItem(item, slot);
                    break;
                }
            }
        }
    }

    private void FillInventorySlotWithItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryIconPrefab, slot.transform);
        InventoryItemIcon inventoryItem = newItem.GetComponent<InventoryItemIcon>();
        inventoryItem.SetupItem(item);
    }
}
