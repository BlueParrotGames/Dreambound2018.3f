using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "Dreambound/Items", order = 1)]
public class Weapon : ScriptableObject
{
    public string name = "New Weapon";
    public enum ItemRarity { Legendary, Epic, Rare, Uncommon, Common }
    public ItemRarity itemRarity;
}
