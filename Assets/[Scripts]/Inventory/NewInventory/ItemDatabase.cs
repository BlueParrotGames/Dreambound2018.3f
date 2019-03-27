using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
    JsonData itemData;

    public static ItemDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    void Start()
    {
        // Change this to a non-streamingassets file later.
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        //Debug.Log(database[1].title);
    }

    public Item FindItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].id == id)
                return database[i];
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            //database.Add(new Item(
            //    (int)itemData[i]["id"],
            //    (string)itemData[i]["title"],
            //    (int)itemData[i]["value"],
            //    (int)itemData[i]["stats"]["strength"],
            //    (int)itemData[i]["stats"]["defense"],
            //    (int)itemData[i]["stats"]["stamina"],
            //    (string)itemData[i]["description"],
            //    (bool)itemData[i]["stackable"],
            //    (int)itemData[i]["rarity"],
            //    (string)itemData[i]["slug"]
            //    ));

            Item item = new Item(
                (int)itemData[i]["id"],
                (string)itemData[i]["title"],
                (int)itemData[i]["value"],
                -1,
                -1,
                -1,
                (string)itemData[i]["description"],
                (bool)itemData[i]["stackable"],
                (int)itemData[i]["rarity"],
                (string)itemData[i]["slug"],
                (string)itemData[i]["type"]
                );

            try
            { item.intelligence = (int)itemData[i]["stats"]["intelligence"]; }
            catch
            { item.intelligence = -1; }

            try
            { item.strength = (int)itemData[i]["stats"]["strength"]; }
            catch
            { item.strength = -1; }

            try
            { item.stamina = (int)itemData[i]["stats"]["stamina"]; }
            catch
            { item.stamina = -1; }

            database.Add(item);
        }
    }
}

[Serializable]
public class Item
{
    public int id;
    public string title;
    public string slug;
    public string description;

    public int value;
    public int strength;
    public int intelligence;
    public int stamina;

    public bool stackable;
    public string itemType;

    public int rarity;
    public Color rarityColor;

    public Sprite sprite;
    public GameObject itemPrefab;
    public string hitPower;

    public Item(int id, string title, int value, int strength, int intelligence, int stamina, string description, bool stackable, int rarity, string slug, string itemType)
    {
        this.id = id;
        this.title = title;
        this.value = value;
        this.strength = strength;
        this.intelligence = intelligence;
        this.stamina = stamina;
        this.description = description;
        this.stackable = stackable;
        this.rarity = rarity;
        this.slug = slug;
        this.itemType = itemType;
        this.sprite = Resources.Load<Sprite>("Textures/Item sprites/" + slug);
        this.itemPrefab = Resources.Load<GameObject>("Items/" + slug);

        switch(rarity)
        {
            case 0:
                {
                    ColorUtility.TryParseHtmlString("#747474", out rarityColor);
                    break;
                }
            case 1:
                {
                    ColorUtility.TryParseHtmlString("#FFFFFF", out rarityColor);
                    break;
                }
            case 2:
                {
                    ColorUtility.TryParseHtmlString("#1CB047", out rarityColor);
                    break;
                }
            case 3:
                {
                    ColorUtility.TryParseHtmlString("#2C44B7", out rarityColor);
                    break;
                }
            case 4:
                {
                    ColorUtility.TryParseHtmlString("#9342B5", out rarityColor);
                    break;
                }
            case 5:
                {
                    ColorUtility.TryParseHtmlString("#FFD600", out rarityColor);
                    break;
                }
        }
    }
    
    public Item()
    {
        this.id = -1;
    }
}

[CustomPropertyDrawer(typeof(Item))]
public class ItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
        if (property.isExpanded)
        {
            //EditorGUI.PropertyField(position, property, label, true);
            Rect buttonRect = new Rect(position.xMin + 30f, position.yMax - 20f, position.width - 30f, 20f);
            if(GUI.Button(buttonRect,"Add me"))
            {
                
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property) + 20f;

        return EditorGUI.GetPropertyHeight(property);
    }
}
