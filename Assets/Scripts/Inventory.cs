using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 25;
    Item[] inventory;
    void Start()
    {
        inventory = new Item[inventorySize];
    }

    void Update()
    {
        
    }

    public void PickUp(Item item)
    {
        if (inventory.Length < 25)
        {
            inventory[inventory.Length + 1] = item;
        }
    }

    private void Drop(){

    }


}
