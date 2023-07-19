using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventoryMaximumCapacity = 25;
    List<Item> inventory;
    void Start()
    {
        inventory = new List<Item>();
    }

    void Update()
    {
        
    }

    public void PickUp(GameObject item)
    {
        if (inventory.Count < inventoryMaximumCapacity)
        {
            inventory.Add(item.GetComponent<Item>());
            Debug.Log(inventory.Count);
            Destroy(item);
        }
    }

    private void Drop(){

    }


}
