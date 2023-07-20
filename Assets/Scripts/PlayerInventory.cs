using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int inventoryMaximumCapacity = 25;

    private List<Item> items = new List<Item>();

    public List<Item> Items { get => items;}

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void PickUp(GameObject item)
    {
        if (Items.Count < inventoryMaximumCapacity)
        {
            Items.Add(item.GetComponent<Item>());
            Debug.Log(Items.Count);
            Destroy(item);
        }
    }

    private void Drop(){

    }


}
