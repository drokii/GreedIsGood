using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryMaximumCapacity = 25;
    [SerializeField] private List<Item> addedInventoryContents = new List<Item>();


    [SerializeField] private List<Item> items = new List<Item>();

    public List<Item> Items { get => items; }
    public List<Item> AddedInventoryContents { get => addedInventoryContents; }

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
            Destroy(pickup);
            addedInventoryContents.Add(item);
        }

    }

    public void Drop(Item itemToDrop)
    {
        foreach(Item item in items)
        {
            if(itemToDrop.Id == item.Id)
            {
                //TODO: Handle quantity. Right now you just toss away the whole stack.
                items.Remove(item);
                // Drop the item by spawning it where the player is, and giving it a tiny force forwards.
                GameObject droppedItem = itemToDrop.Prefab;
                Instantiate(droppedItem, transform.position, transform.rotation);
                Rigidbody droppedItemRigidbody = droppedItem.GetComponent<Rigidbody>();
                droppedItemRigidbody.AddForce(transform.forward * 3f, ForceMode.Impulse);
                return;
            }
        }      
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
