using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<InventorySlot> inventorySlots;

    public GameObject inventorySlotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        loadItemsFromPlayerInventory();
    }

    private void loadItemsFromPlayerInventory()
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
        GameObject newItem = Instantiate(inventorySlotPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.SetupItem(item);
    }
}
