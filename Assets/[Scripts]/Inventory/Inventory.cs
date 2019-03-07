using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static Inventory instance;
    public int space = 20;

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this);
    }   

    public bool Add(Item i)
    {
        if(!i.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Out of space in inventory");
                return false;
            }

            items.Add(i);
            if(onInventoryChangedCallback != null)
                onInventoryChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item i)
    {
        items.Remove(i);

        if (onInventoryChangedCallback != null)
            onInventoryChangedCallback.Invoke();
    }
}
