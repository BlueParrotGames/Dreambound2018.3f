using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public int index;
    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.Find("Scene").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        Debug.Log("Dropped!");
        if (inventory.items[index].id == -1)
        {
            inventory.items[droppedItem.slotIndex] = new Item();
            inventory.items[index] = droppedItem.item;
            droppedItem.slotIndex = index;
        }
        else if (droppedItem.slotIndex != index)
        {
            Transform item = transform.GetChild(0);
            item.GetComponent<ItemData>().slotIndex = droppedItem.slotIndex;
            item.transform.SetParent(inventory.slots[droppedItem.slotIndex].transform);
            item.transform.position = inventory.slots[droppedItem.slotIndex].transform.position;

            inventory.items[droppedItem.slotIndex] = item.GetComponent<ItemData>().item;
            inventory.items[index] = droppedItem.item;
            droppedItem.slotIndex = index;
        }
    }
}
