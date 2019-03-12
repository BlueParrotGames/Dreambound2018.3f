using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySpace = 20;

    public GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public int slotAmount;
    public int inventoryCount;
    public int inventoryMax;

    public bool showInventory = true;
    public bool inventoryFull = false;

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
        if(inventoryCount < inventoryMax)
        {
            inventoryFull = false;

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
                    if (items[i].title == null)
                    {
                        items[i] = item;
                        GameObject itemObj = Instantiate(inventoryItem, slots[i].transform);
                        ItemData data = itemObj.GetComponent<ItemData>();
                        data.item = item;
                        data.slotIndex = i;
                        // set data player
                        // set data player inventory
                        itemObj.name = item.title;
                        slots[i].name = itemObj.name + " Slot";
                        itemObj.name = item.title + " Item";
                        data.iconRenderer.sprite = item.sprite;
                        data.rarityRenderer.color = new Color(item.rarityColor.r, item.rarityColor.g, item.rarityColor.b, 0.35294f);
                        itemObj.transform.localPosition = Vector2.zero;

                        inventoryCount++;
                        if (inventoryCount == inventoryMax)
                            inventoryFull = true;
                        break;
                    }
                }
            }
        }
        else if(inventoryCount == inventoryMax)
        {
            inventoryFull = true;
            Debug.Log("Inventory full");
        }

    }

    public void RemoveItem(int index)
    {
        items[index] = new Item();
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
