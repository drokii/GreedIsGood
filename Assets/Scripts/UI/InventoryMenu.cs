
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventorySlot[] inventorySlots;

    public GameObject inventoryIconPrefab;

    void OnEnable()
    {
        if (playerInventory.AddedInventoryContents.Count != 0)
        {
            synchronizeWithPlayerInventory();
        }
        
    }

    private void synchronizeWithPlayerInventory()
    {
        foreach (Item item in playerInventory.AddedInventoryContents)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (slot.CanAcceptItem(item))
                {
                    FillInventorySlotWithItem(item, slot);
                    break;
                }
            }
        }

        playerInventory.AddedInventoryContents.Clear();
    }

    private void clearInventorySlots()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.transform.childCount != 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
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
