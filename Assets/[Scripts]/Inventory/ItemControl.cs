using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : Interactable
{
    public Item item;
    public override void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool pickedUp = Inventory.instance.Add(item);
        if(pickedUp)
        {
            Destroy(gameObject);
        }
    }
}
