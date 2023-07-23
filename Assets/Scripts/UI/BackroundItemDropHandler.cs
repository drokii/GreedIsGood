using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackroundItemDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private PlayerInventory inventory;
    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<InventoryItemIcon>().item;
        inventory.Drop(item);
        Destroy(eventData.pointerDrag);
    }

}
