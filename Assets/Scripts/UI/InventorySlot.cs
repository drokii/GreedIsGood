using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty())
        {
            InventoryItemIcon inventoryItem = eventData.pointerDrag.GetComponent<InventoryItemIcon>();
            inventoryItem.parentAfterDrag = transform;
        }

    }

    private bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    public bool CanAcceptItem(Item item)
    {
        if (IsEmpty()) return true;
        Debug.Log(transform.childCount);

        Item itemInSlot = transform.GetChild(0).GetComponent<InventoryItemIcon>().item;

        if (itemInSlot.Stackable && item.GetType() == itemInSlot.GetType())
        {
            return true;
        }

        return false;
    }

}

