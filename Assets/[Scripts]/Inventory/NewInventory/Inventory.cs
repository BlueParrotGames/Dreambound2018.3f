using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySpace = 20;
    [SerializeField] int inventoryCount;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject inventorySlot;
    [SerializeField] GameObject inventoryItem;
    [SerializeField] GameObject equipmentPanel;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    [SerializeField] bool showInventory = true;
    [SerializeField] bool inventoryFull = false;

    [SerializeField] ItemDatabase itemDatabase;
    [SerializeField] GameObject player;
    Player p;

    public SkinnedMeshRenderer avatar;
    SkinnedMeshRenderer[] currentMeshes;
    public static Inventory instance;

    //public SkinnedMeshRenderer debugMesh;
    public List<CharacterSlot> equipSlots = new List<CharacterSlot>();

    private void Awake()
    {
        equipSlots.Add(new CharacterSlot(1, "helmet", false));
        equipSlots.Add(new CharacterSlot(2, "chestplate", false));
        equipSlots.Add(new CharacterSlot(3, "leggings", false));
        equipSlots.Add(new CharacterSlot(4, "boots", false));
        equipSlots.Add(new CharacterSlot(5, "gloves", false));

        //equipSlots.Add(new EquipmentSlot(6, "mainhand", false));
        //equipSlots.Add(new EquipmentSlot(7, "offhand", false));
    }

    void Start()
    {
        p = player.GetComponent<Player>();

        if (p.networkObject.IsOwner)
        {
            instance = this;
            if (inventoryPanel == null)
                inventoryPanel = GameObject.Find("Inventory Panel");

            if (equipmentPanel == null)
                equipmentPanel = GameObject.Find("Equipment Panel");

            if (itemDatabase == null)
                itemDatabase = GameManager.instance.GetComponent<ItemDatabase>();

            for (int i = 0; i < inventorySpace; i++)
            {
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlot, inventoryPanel.transform.Find("Slot Panel").transform));
                Slot s = slots[i].GetComponent<Slot>();
                s.index = i;
                //s.player = gameObject;
            }

            for (int i = 0; i < equipSlots.Count; i++)
            {
                //Debug.Log(equipSlots[i].slotName + " " + equipSlots[i].equipmentStatus);
                Transform t = equipmentPanel.transform.Find("Slot Panel").GetChild(i);
                t.name = equipSlots[i].slotName + " Slot";
            }
        }

        //AddItem(3);
        //Equip();
    }
    /// <summary>
    /// Add an item to your inventory.
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        Item item = ItemDatabase.instance.FindItemByID(id);
        if(inventoryCount < inventorySpace)
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
                        if (inventoryCount == inventorySpace)
                            inventoryFull = true;
                        break;
                    }
                }
            }
        }
        else if(inventoryCount == inventorySpace)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = !showInventory;

            inventoryPanel.SetActive(showInventory);
            //equipmentPanel.SetActive(showInventory);
        }
    }

    public void Equip(Item item)
    {
        for (int i = 0; i < equipSlots.Count; i++)
        {
            if(equipSlots[i].slotName == item.itemType)
            {
                if(equipSlots[i].equipmentStatus == true)
                {
                    Debug.Log("Already something equipped in this slot!");
                    return;
                }
                else
                {
                    equipSlots[i].equipmentStatus = true;
                    break;
                }
            }
        }

        SkinnedMeshRenderer characterRenderer = avatar;
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in characterRenderer.bones)
        {
            boneMap[bone.name] = bone;
        }

        SkinnedMeshRenderer gearRenderer = Instantiate<SkinnedMeshRenderer>(item.mesh, transform);
        gearRenderer.updateWhenOffscreen = true;
        Transform[] boneArray = gearRenderer.bones;
        for (int i = 0; i < boneArray.Length; ++i)
        {
            string boneName = boneArray[i].name;
            if (!boneMap.TryGetValue(boneName, out boneArray[i]))
            {
                Debug.LogWarning("Failed to get bone: " + boneName);
            }
        }

        gearRenderer.bones = boneArray; //take effect
        //SkinnedMeshRenderer skinnedMesh = Instantiate<SkinnedMeshRenderer>(tempMesh, transform);
        //skinnedMesh.transform.parent = targetMesh.transform.parent;

        //skinnedMesh.bones = targetMesh.bones;
        //skinnedMesh.rootBone = targetMesh.rootBone;
    }
}

[Serializable]
public class CharacterSlot
{
    public int index;
    public string slotName;
    public bool equipmentStatus;


    public CharacterSlot(int index, string slotName, bool equipmentStatus)
    {
        this.index = index;
        this.slotName = slotName;
        this.equipmentStatus = equipmentStatus;
    }
        
}

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Inventory t = (Inventory)target;
        //if(GUILayout.Button("Equip tempMesh"))
        //{
        //    t.Equip(t.debugMesh);
        //}
    }
}