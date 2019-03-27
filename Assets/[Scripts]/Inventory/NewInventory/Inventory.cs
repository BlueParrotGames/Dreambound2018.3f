using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int inventorySpace = 20;

    public GameObject inventoryPanel;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    public int inventoryCount;

    public bool showInventory = true;
    public bool inventoryFull = false;

    public ItemDatabase itemDatabase;
    public GameObject player;

    public SkinnedMeshRenderer avatar;
    SkinnedMeshRenderer[] currentMeshes;

    public SkinnedMeshRenderer debugMesh;


    void Start()
    {
        if (inventoryPanel == null)
            inventoryPanel = GameObject.Find("Inventory Panel");

        if (slotPanel == null)
            slotPanel = GameObject.Find("Inventory Panel/Slot Panel");

        if (itemDatabase == null)
            itemDatabase = GameManager.instance.GetComponent<ItemDatabase>();

        for (int i = 0; i < inventorySpace; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot, slotPanel.transform));
            Slot s = slots[i].GetComponent<Slot>();
            s.index = i;
            s.player = gameObject;
        }

        AddItem(3);
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

    public void Equip(SkinnedMeshRenderer debugMesh)
    {
        SkinnedMeshRenderer characterRenderer = avatar;
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in characterRenderer.bones)
        {
            boneMap[bone.name] = bone;
        }

        SkinnedMeshRenderer gearRenderer = Instantiate<SkinnedMeshRenderer>(debugMesh, transform);
        gearRenderer.updateWhenOffscreen = true;
        Transform[] boneArray = gearRenderer.bones;
        for (int i = 0; i < boneArray.Length; ++i)
        {
            string boneName = boneArray[i].name;
            if (!boneMap.TryGetValue(boneName, out boneArray[i]))
            {
                Debug.LogWarning("Failed to get bone: " + boneName);
                //Debug.Break();
            }
        }

        gearRenderer.bones = boneArray; //take effect
        //SkinnedMeshRenderer skinnedMesh = Instantiate<SkinnedMeshRenderer>(tempMesh, transform);
        //skinnedMesh.transform.parent = targetMesh.transform.parent;

        //skinnedMesh.bones = targetMesh.bones;
        //skinnedMesh.rootBone = targetMesh.rootBone;
    }
}

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Inventory t = (Inventory)target;
        if(GUILayout.Button("Equip tempMesh"))
        {
            t.Equip(t.debugMesh);
        }
    }
}