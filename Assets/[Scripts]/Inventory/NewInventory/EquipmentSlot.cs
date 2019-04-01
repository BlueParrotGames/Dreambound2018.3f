using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{

    public new void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        Debug.Log("Dropped!");
        if (Inventory.instance.items[index].id == -1)
        {
            Inventory.instance.items[droppedItem.slotIndex] = new Item();
            Inventory.instance.items[index] = droppedItem.item;
            droppedItem.slotIndex = index;
        }
        else if (droppedItem.slotIndex != index)
        {
            Transform item = transform.GetChild(0);
            item.GetComponent<ItemData>().slotIndex = droppedItem.slotIndex;
            item.transform.SetParent(/*Inventory.instance.slots[droppedItem.slotIndex].*/transform);
            item.transform.position = /*Inventory.instance.slots[droppedItem.slotIndex].*/transform.position;

            Inventory.instance.items[droppedItem.slotIndex] = item.GetComponent<ItemData>().item;
            Inventory.instance.items[index] = droppedItem.item;
            droppedItem.slotIndex = index;
        }
    }
}
