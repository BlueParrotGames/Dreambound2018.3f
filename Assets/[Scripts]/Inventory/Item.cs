using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Dreambound/Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public bool isDefaultItem;

    public enum ItemRarity { Legendary, Epic, Rare, Uncommon, Common }
    public ItemRarity itemRarity;

}
