using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySpace = 20;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject slotPanel;
    [SerializeField] GameObject inventorySlot;
    [SerializeField] GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        if (inventoryPanel == null)
            inventoryPanel = GameObject.Find("Inventory Panel");

        if (slotPanel == null)
            slotPanel = GameObject.Find("Inventory Panel/Slot Panel");

        for (int i = 0; i < inventorySpace; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot, slotPanel.transform));
            slots[i].GetComponent<Slot>().index = i;
        }

        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
    }       

    public void AddItem(int id)
    {
        Item item = ItemDatabase.instance.FindItemByID(id);

        if (item.stackable && FindItemInInventory(item))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].id == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount += 1;
                    data.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = (data.amount + 1).ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].id == -1)
                {
                    items[i] = item;
                    GameObject itemObj = Instantiate(inventoryItem, slots[i].transform);
                    ItemData data = itemObj.GetComponent<ItemData>();
                    data.item = item;
                    data.slotIndex = i;
                    data.iconRenderer.sprite = item.sprite;
                    data.rarityRenderer.color = new Color(item.rarityColor.r, item.rarityColor.g, item.rarityColor.b, 0.35294f);
                    itemObj.transform.localPosition = Vector2.zero;
                    itemObj.name = item.title;
                    break;
                }
            }
        }
    }

    bool FindItemInInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == item.id)
                return true;
        }
        return false;
    }
}
