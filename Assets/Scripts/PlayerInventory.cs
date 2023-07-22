using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryMaximumCapacity = 25;
    [SerializeField] private List<Item> addedInventoryContents = new List<Item>();
    [SerializeField] private List<Item> droppedInventoryContents = new List<Item>();


    [SerializeField] private List<Item> items = new List<Item>();

    public List<Item> Items { get => items; }
    public List<Item> AddedInventoryContents { get => addedInventoryContents; }
    public List<Item> DroppedInventoryContents { get => droppedInventoryContents; }

    public void PickUp(GameObject pickup)
    {
        Item item = pickup.GetComponent<ItemContainer>().Item;

        if (items.Count > inventoryMaximumCapacity)
        {
            //TODO: Inventory Full Alert
            Debug.Log("Inventory Is Full!");
            return;
        }

        if (ItemTypeAlreadyInInventory(item) && item.Stackable)
        {
            AddToItemQuantity(item);
            Destroy(pickup);
            addedInventoryContents.Add(item);
        }
        else
        {
            items.Add(item);
            Debug.Log(items.Count);
            Destroy(pickup);
            addedInventoryContents.Add(item);
        }

    }

    public void Drop()
    {

    }

    private bool ItemTypeAlreadyInInventory(Item itemToCompare)
    {
        foreach (Item item in items)
        {
            if (itemToCompare.GetType() == item.GetType())
            {
                return true;
            }
        }
        return false;
    }

    private void AddToItemQuantity(Item itemToAdd)
    {
        foreach (Item item in items)
        {
            if (itemToAdd.GetType() == item.GetType() && item.Stackable)
            {
                item.quantity = +itemToAdd.quantity;
            }
        }
    }
}
