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

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

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
        }

        AddItem(1);
    }       

    void AddItem(int id)
    {
        Item item = ItemDatabase.instance.FindItemByID(id);
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == -1)
            {
                items[i] = item;
                GameObject itemObj = Instantiate(inventoryItem, slots[i].transform);
                itemObj.GetComponent<Image>().sprite = item.sprite;
                itemObj.transform.localPosition = Vector2.zero;
                itemObj.name = item.title;
                break;
            }
        }
    }
}
