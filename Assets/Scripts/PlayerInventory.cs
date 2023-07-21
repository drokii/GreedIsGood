using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int inventoryMaximumCapacity = 25;

    private List<Item> items = new List<Item>();

    public List<Item> Items { get => items; }

    public void PickUp(GameObject pickup)
    {
        Item item = pickup.GetComponent<Item>();

        if (items.Count > inventoryMaximumCapacity)
        {
            //TODO: Inventory Full Alert
            Debug.Log("Inventory Is Full!");
            return;
        }

        if (ItemTypeAlreadyInInventory(item))
        {
            AddToItemQuantity(item);
            Destroy(pickup);
        }
        else
        {
            items.Add(item);
            Debug.Log(items.Count);
            Destroy(pickup);
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
            if (itemToAdd.GetType() == item.GetType())
            {
                item.quantity = +itemToAdd.quantity;
            }
        }
    }
}
