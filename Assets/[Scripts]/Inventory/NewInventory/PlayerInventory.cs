using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : Inventory
{
    public GameObject equipmentPanel;

    void Start()
    {
        inventoryMax = 20;
        InstantiateInventory();
        InventoryPositionAndSize();
        CreateSlots();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;

            inventoryPanel.SetActive(showInventory);
            equipmentPanel.SetActive(showInventory);
        }
    }

    void InstantiateInventory()
    {

    }

    void InventoryPositionAndSize()
    {

    }

    void CreateSlots()
    {

    }
}
