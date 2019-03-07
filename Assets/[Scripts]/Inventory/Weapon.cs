using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Dreambound/Inventory/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string name = "New Weapon";
    public enum ItemRarity { Legendary, Epic, Rare, Uncommon, Common }
    public ItemRarity itemRarity;
    public enum GripType { OneHanded, TwoHanded, Staff }
    public GripType gripType;
    public GameObject prefab;

    public Transform leftHandGrip;
    public Transform rightHandGrip;

}
